#include "cbase.h"
#include "C_ComputerParser.h"
#include "C_ComputerFunc.h"

C_ComputerFunc* C_ComputerParser::GetComputerFunc()
{
	return this->pComputer->GetClass();
}

C_ComputerParser::C_ComputerParser( vgui::RichText *vguiRichText, vgui::TextEntry *vguiCmd )
{
	DevMsg("Built a parser!\n");
	this->pComputer = new C_ComputerFunc( vguiRichText, vguiCmd );
	//this->m_vguiRichText->SetText("HEJSAN DIN FETA KOJAVEL");
	this->Parse();
}

C_ComputerParser::C_ComputerParser()
{
	DevMsg("Built a parser!\n");
	const char *pGameDir = engine->GetGameDirectory();
	this->pComputer = new C_ComputerFunc();
	this->Parse();
}

void C_ComputerParser::Parse()
{

	KeyValues *pRoot = new KeyValues( "Computer" );
	pRoot->LoadFromFile( g_pFullFileSystem, this->m_Script.c_str(), "MOD" );
	//DevMsg("%s \n", pRoot->GetString("screen saver", "should be santa monica bloodbank"));
	//DevMsg("%s \n", pRoot->GetString("brackets", "should be []"));

	//note: UTIL_VarArgs in server dll but just VarArgs in client dll
	KeyValues *pLogonScreen = pRoot->FindKey( "LogonScreen" );
	int nLineNum = 0;
	char szLineText[128];
	stLogonScreen logon;
	
	Q_snprintf(szLineText, sizeof(szLineText), pLogonScreen->GetString( VarArgs("line%i", nLineNum), "none"));
	logon.strLines.append(szLineText);
	logon.strLines.append("\n");
	nLineNum++;
	while( strcmp( szLineText, "none" ) != 0 )
	{	
		//DevMsg("%s \n", pLogonScreen->GetString( VarArgs( "line%i", nLineNum ), "none" ) );
		Q_snprintf(szLineText, sizeof(szLineText), pLogonScreen->GetString(VarArgs("line%i", nLineNum), "none"));
		if( strcmp( szLineText, "none" ) != 0 )
		{
			logon.strLines.append(szLineText);
			logon.strLines.append("\n");
			nLineNum++;
		}
	}
	pComputer->AddLogon( logon.strLines );

	//pLogonScreen->deleteThis();

	//TEST:
	/*stSubDir *s = new stSubDir;
	Q_snprintf(s->szName,sizeof(s->szName),"name here");
	Q_snprintf(s->szPass,sizeof(s->szPass),"password is here");
	Q_snprintf(s->szDesc,sizeof(s->szDesc),"the description");
	stComputerFunc *pFunction = new stComputerFunc;
	Q_snprintf(pFunction->szCommandName,sizeof(pFunction->szCommandName),"name here in function");
	Q_snprintf(pFunction->szDesc,sizeof(pFunction->szDesc),"the description in function");
	s->vecTest.push_back(pFunction);
	pComputer->AddSubDir( s );*/
	//ENDTEST

	//C_ComputerFunc *pComputer = new C_ComputerFunc( this->m_vguiRichText, logon.strLines );

	for ( KeyValues *pKey = pRoot->GetFirstTrueSubKey(); pKey; pKey = pKey->GetNextTrueSubKey() )
	{
		stSubDir *s = new stSubDir;
		Q_snprintf(s->szName,sizeof(s->szName),pKey->GetString("name", "none"));
		Q_snprintf(s->szPass,sizeof(s->szPass),pKey->GetString("password", "none"));
		Q_snprintf(s->szDesc,sizeof(s->szDesc),pKey->GetString("description", "none"));
		s->bEnteredPass = false;
		//this check shouldn't really be here for the pass
		if( strcmp(s->szName, "none") != 0 && strcmp(s->szPass, "none") != 0 && strcmp(s->szDesc, "none") != 0 )
		{
			for( KeyValues *pSubKey = pKey->GetFirstTrueSubKey(); pSubKey; pSubKey = pSubKey->GetNextTrueSubKey())
			{
				//means its a Function key
				if( strcmp(pSubKey->GetName(), "Function") == 0)
				{
					stComputerFunc *c = new stComputerFunc;
					Q_snprintf(c->szCmdName,sizeof(c->szCmdName),pSubKey->GetString("name", "none"));
					Q_snprintf(c->szDesc,sizeof(c->szDesc),pSubKey->GetString("description", "none"));
					Q_snprintf(c->szRunText,sizeof(c->szRunText),pSubKey->GetString("runtext", "none"));
					std::vector<stComputerFunc*>::iterator it;
					it = s->vecFunctions.begin();
					//it = s->vecFunctions.insert( it, c );
				}
			}
			pComputer->AddSubDir( s );
		}
	}

	pComputer->Show();
}

C_ComputerParser::~C_ComputerParser()
{
	DevMsg("Destroyed a parser!\n");
}

void C_ComputerParser::Say(const char *what)
{
	DevMsg("Saying: %s \n", what);
}