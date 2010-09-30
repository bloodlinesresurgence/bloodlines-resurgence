#include "cbase.h"
#include "C_ComputerFunc.h"
#include "IVampireComputer.h"

/*
TODO: Add support for passwords!
*/

C_ComputerFunc* C_ComputerFunc::GetClass()
{
	return this;
}

void C_ComputerFunc::Say( const char *szSay )
{
	DevMsg("ComputerFunc saying: %s \n", szSay);
}

void C_ComputerFunc::AddSubDir( stSubDir *SubDir )
{
	m_SubDir.push_back( SubDir );
}

void C_ComputerFunc::Show()
{
	unsigned int i, j;
	for( i = 0; i < m_SubDir.size(); i++ )
	{
		DevMsg("Outer parts: %s \n", m_SubDir.at(i)->szDesc);
		for( j = 0; j < m_SubDir.at(i)->vecFunctions.size(); j++ )
		{
			DevMsg("Inner parts: %s \n", m_SubDir.at(i)->vecFunctions.at(j)->szCmdName);
		}
	}

	this->ShowMainMenu();
}

void C_ComputerFunc::ShowMainMenu()
{
	std::string strLogon;
	strLogon.append( this->m_strLogon );
	unsigned int i, j;
	for( i = 0; i < m_SubDir.size(); i++ )
	{
		strLogon.append( m_SubDir.at(i)->szName );
		strLogon.append( "\n" );
	}
	this->m_vguiText->SetText( strLogon.c_str() );
	this->m_vguiCmd->SetText("");
	this->m_nCurrentFunc = -1;
	this->m_nCurrentSubDir = -1;
}

void C_ComputerFunc::ShowSubDir( int nSubDir )
{
	DevMsg("Showing dir %d \n", nSubDir);
	std::string tmp;
	//password-check
	if ( stricmp( m_SubDir.at(nSubDir)->szPass, "none") != 0 && !m_SubDir.at(nSubDir)->bEnteredPass )
	{
		this->ShowPasswordPrompt( nSubDir, false );
	}
	else
	{
		tmp.append( "Available directories: \n" );
		tmp.append( "Home\n" );
		for (unsigned int h = 0; h < m_SubDir.size(); h++)
		{
			tmp.append( m_SubDir.at(h)->szName );
			tmp.append( "\n" );
		}
		tmp.append( VarArgs( "Available commands in %s:\n", m_SubDir.at(nSubDir)->szName ) );
		for (unsigned int i = 0; i < m_SubDir.at(nSubDir)->vecFunctions.size(); i++)
		{
			tmp.append( m_SubDir.at(nSubDir)->vecFunctions.at(i)->szCmdName );
			tmp.append( "\n" );
		}
		this->m_nCurrentSubDir = nSubDir;
		this->m_vguiText->SetText( tmp.c_str() );
		this->m_vguiCmd->SetText("");
	}
}

void C_ComputerFunc::ShowList()
{
	//subject to change?
	this->ShowMainMenu();
}

void C_ComputerFunc::Enter()
{
	this->m_vguiCmd->SetEditable( true );
	this->m_vguiCmd->SetText("");
	this->m_bOnlyEnter = false;
	if( this->m_bFallback )
	{
		this->ShowSubDir( m_nCurrentSubDir );
		this->m_bFallback = false;
	}
	else
		this->ShowMainMenu();
}

bool C_ComputerFunc::GetOnlyEnter()
{
	return this->m_bOnlyEnter;
}

bool C_ComputerFunc::IsPasswordReq()
{
	return this->m_bPasswordReq;
}

void C_ComputerFunc::ShowError( const char *szCmd )
{
	this->m_vguiText->SetText( VarArgs( "%s is not a command!\n", szCmd ) );
	this->m_vguiCmd->SetText( "Press [ENTER] to continure" );
	this->m_vguiCmd->SetEditable( false );
	if( m_nCurrentSubDir != -1 )
		this->m_bFallback = true;
	else
		this->m_bFallback = false;
	this->m_bOnlyEnter = true;
}

void C_ComputerFunc::ShowPasswordPrompt( int nSubDir, bool bRecursed )
{
	if(bRecursed)
	{
		std::string tmp;
		tmp.append( VarArgs( "%s requires a password to continue.\n", m_SubDir.at(nSubDir)->szName ) );
		tmp.append( "Wrong password!\n" );
		this->m_vguiText->SetText( tmp.c_str() );
	}
	else
	{
		this->m_vguiText->SetText( VarArgs( "%s requires a password to continue.\n", m_SubDir.at(nSubDir)->szName ) );
	}
	this->m_bPasswordReq = true;
	this->m_nPasswordReq = nSubDir;
	this->m_vguiCmd->SetText("");
}

void C_ComputerFunc::HandlePassword( const char *szCmd )
{
	bool bFound = false;

	if( !this->m_bPasswordReq )
		return;

	if( strcmp( szCmd, m_SubDir.at(m_nPasswordReq)->szPass ) == 0 )
	{
		m_SubDir.at(m_nPasswordReq)->bEnteredPass = true;
		this->m_vguiCmd->SetText("");
		this->m_bPasswordReq = false;
		this->ShowSubDir(m_nPasswordReq);
		bFound = true;
	}
	else
	{
		for( unsigned int i = 0; i < m_SubDir.size(); i++)
		{
			if( stricmp( szCmd, m_SubDir.at(i)->szName ) == 0 )
			{
				this->m_vguiCmd->SetText("");
				this->m_bPasswordReq = false;
				this->ShowSubDir( i );
				bFound = true;
				break;
			}
		}
		if( stricmp( szCmd, "home" ) == 0 )
		{
			this->ShowMainMenu();
			bFound = true;
		}
		if(!bFound)
		{
			this->m_bPasswordReq = true;
			this->ShowPasswordPrompt( m_nPasswordReq, true );
		}
	}
}

void C_ComputerFunc::ShowHelp()
{
	this->m_vguiText->SetText( this->m_ComputerText.strHelp.c_str() );
	this->m_vguiCmd->SetText( "Press [ENTER] to continue" );
	this->m_vguiCmd->SetEditable( false );
	this->m_bOnlyEnter = true;
}

void C_ComputerFunc::HandleCmd( const char *szCmd )
{
	//stricmp!
	DevMsg("should handle: %s \n", szCmd);
	bool bFound = false;
	
	if( stricmp( szCmd, "" ) == 0 )
		return;
	if( stricmp( szCmd, "home" ) == 0 )
	{
		this->ShowMainMenu(); 
		this->m_vguiCmd->SetText("");
		return;
	}
	if( stricmp( szCmd, "help" ) == 0 )
	{
		this->ShowHelp();
		return;
	}
	if( stricmp( szCmd, "list" ) == 0 )
	{
		this->ShowList();
		this->m_vguiCmd->SetText("");
		return;
	}
	if( stricmp( szCmd, "exit" ) == 0 )
	{
		vampireComputer->Deactivate();
		return;
	}
	if( this->m_nCurrentSubDir == -1 && this->m_nCurrentFunc == -1 )
	{
		//find the right subdir to view
		for (unsigned int i = 0; i < m_SubDir.size(); i++ )
		{
			if( stricmp( szCmd, m_SubDir.at(i)->szName ) == 0 )
			{
				this->ShowSubDir( i );
				bFound = true;
				//we should stop if we found a match
				break;
			}
		}
		if(!bFound)
			this->ShowError( szCmd );
	}
	else
	{
		//if we are trying to access the same directory as the one we are in
		if( stricmp ( szCmd, m_SubDir.at(m_nCurrentSubDir)->szName ) == 0 )
		{
			//ShowSubDir( m_nCurrentSubDir );
			this->m_vguiCmd->SetText("");
			return;
		}
		else
		{
			//the runtext part
			for (unsigned int i = 0; i < m_SubDir.at(this->m_nCurrentSubDir)->vecFunctions.size(); i++)
			{
				if( stricmp( szCmd, m_SubDir.at(this->m_nCurrentSubDir)->vecFunctions.at(i)->szCmdName ) == 0 )
				{
					std::string tmp;
					tmp.append( this->m_SubDir.at(m_nCurrentSubDir)->vecFunctions.at(i)->szRunText );
					tmp.append( "\n" );
					//tmp.append( this->m_SubDir.at(m_nCurrentSubDir)->szName );
					this->m_vguiText->SetText( tmp.c_str() );
					this->m_nCurrentFunc = i;
					bFound = true;
					this->m_vguiCmd->SetText( "Press [ENTER] to continue" );
					this->m_vguiCmd->SetEditable( false );
					this->m_bOnlyEnter = true;
					this->m_bFallback = true;
					//we should stop if we found a match
					break;
				}
			}
			for (unsigned int i = 0; i < m_SubDir.size(); i++)
			{
				if( stricmp( szCmd, m_SubDir.at(i)->szName ) == 0 )
				{
					this->ShowSubDir( i );
					bFound = true;
					break;
				}
			}
			if(!bFound)
				this->ShowError( szCmd );
		}
	}
	/*if( stricmp( szCmd, m_SubDir.at(m_nCurrentSubDir)->szName ) != 0 && 
		stricmp( szCmd, m_SubDir.at(m_nCurrentSubDir)->vecFunctions.at(m_nCurrentFunc)->szCmdName ) != 0 )
	{
		DevMsg("THERE NO SUCH COMMAND! \n");
	}*/
}

void C_ComputerFunc::AddLogon( std::string strLogon )
{
	this->m_strLogon = strLogon;
}

C_ComputerFunc::C_ComputerFunc( vgui::RichText *vguiText, vgui::TextEntry *vguiCmd )
{
	this->m_bPasswordReq = false;
	this->m_bFallback = false;
	this->m_bOnlyEnter = false;
	this->m_vguiText = vguiText;
	this->m_vguiCmd = vguiCmd;
	this->m_nCurrentFunc = -1;
	this->m_nCurrentSubDir = -1;
	this->m_ComputerText.strHelp = "Type 'list' to get the available menus and commands.\n\nMenu names are listed in brackets. Type the name of a menu to switch to that menu.\n\nCommand names are listed after menu names. Type the name of a command to run that command.\n\nDifferent menus have different availabe commands.";
	this->m_ComputerText.strInvalidCmd = "Type 'list' to get the available menus and commands.\nType 'help' for assistance.";
}

C_ComputerFunc::C_ComputerFunc( vgui::RichText *vguiText, std::string strLogon )
{
	this->m_bPasswordReq = false;
	this->m_bFallback = false;
	this->m_bOnlyEnter = false;
	this->m_vguiText = vguiText;
	this->m_nCurrentFunc = -1;
	this->m_nCurrentSubDir = -1;
	this->m_strLogon = strLogon;
	this->m_ComputerText.strHelp = "Type 'list' to get the available menus and commands.\n\nMenu names are listed in brackets. Type the name of a menu to switch to that menu.\n\nCommand names are listed after menu names. Type the name of a command to run that command.\n\nDifferent menus have different availabe commands.";
	this->m_ComputerText.strInvalidCmd = "Type 'list' to get the available menus and commands.\nType 'help' for assistance.";
}

C_ComputerFunc::C_ComputerFunc()
{
	DevMsg("Got a computerfunction!\n");
}

C_ComputerFunc::~C_ComputerFunc()
{
	DevMsg("Destroyed computerfunc\n");
}