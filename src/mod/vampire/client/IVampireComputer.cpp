//The following include files are necessary to allow your vampireComputer.cpp to compile.
#include "cbase.h"
#include "IVampireComputer.h"
using namespace vgui;
#include <vgui/IVGui.h>
#include <vgui/IScheme.h>
#include <vgui_controls/Button.h>
#include <vgui_controls/Frame.h>
#include <vgui_controls/TextEntry.h> 
#include <vgui_controls/RichText.h>
#include <vgui/KeyCode.h>
#include "C_ComputerParser.h"
#include "C_ComputerFunc.h"

ConVar cl_computer_script("cl_computer_script", "", FCVAR_CLIENTDLL, "Sets the current computer script");

 //CVampireComputer class: Tutorial example class
 class CVampireComputer : public vgui::Frame
 {
 	DECLARE_CLASS_SIMPLE(CVampireComputer, vgui::Frame); 
 	//CVampireComputer : This Class / vgui::Frame : BaseClass
 
 	CVampireComputer(vgui::VPANEL parent); 	// Constructor
 	~CVampireComputer(){};				// Destructor
	void Deactivate (void) {
		SetVisible(false);
	}

	void refreshScript ()
	{
		this->m_Parser->setScript(cl_computer_script.GetString());
	}

 protected:
 	//VGUI overrides:
 	virtual void OnTick();
 	virtual void OnCommand(const char* pcCommand);
	virtual void OnKeyCodePressed(KeyCode code);
	//virtual void OnMessage(const KeyValues* data);

 private:
 	//Other used VGUI control Elements:
	vgui::TextEntry* m_pTime; // Panel class declaration, private section
	vgui::Button* m_Button;
	vgui::RichText* m_TextPanel;
	C_ComputerFunc *pComputerFunc;

	C_ComputerParser* m_Parser;
	//MESSAGE_FUNC_INT_INT( OnCursorMoved, "OnCursorMoved", x, y );
	//MESSAGE_FUNC_PARAMS( OnMessage, "OnMessage", data );
 
 };

 // Constuctor: Initializes the Panel
CVampireComputer::CVampireComputer(vgui::VPANEL parent) : BaseClass(NULL, "vampireComputer")
{

	SetParent( parent );
 
	SetKeyBoardInputEnabled( true );
	SetMouseInputEnabled( true );
 
	SetTitle("Computer", true);
	SetProportional( true );
	SetTitleBarVisible( false );
	SetMinimizeButtonVisible( false );
	SetMaximizeButtonVisible( false );
	SetCloseButtonVisible( false );
	SetSize( vgui::scheme()->GetProportionalScaledValue(640) , vgui::scheme()->GetProportionalScaledValue(480) );
	SetPos( 0, 0 );
	SetSizeable( false );
	SetMoveable( false );
	SetVisible( true );
 
	m_pTime = new vgui::TextEntry(this, "MyTextEntry");
	m_pTime->SetPos( vgui::scheme()->GetProportionalScaledValue(0), vgui::scheme()->GetProportionalScaledValue(468) );
	m_pTime->SetSize( vgui::scheme()->GetProportionalScaledValue(640), 22 ); //dont scale height!
	m_pTime->AddActionSignalTarget( this );
	m_pTime->SendNewLine( true );
	m_pTime->MoveToFront();
	m_pTime->RequestFocus();

	m_Button = new vgui::Button(this, "MyButton", "Submit", this);
	m_Button->SetPos( -100, -100 ); // Offscreen
	m_Button->SetSize( vgui::scheme()->GetProportionalScaledValue(50), 22 ); //dont scale height!
	m_Button->AddActionSignalTarget( this );
	m_Button->AddKeyBinding( "enter", KEY_ENTER, 0 );
	m_Button->SetCommand( "enter" );

	m_TextPanel = new vgui::RichText(this, "MyRichText");
	m_TextPanel->SetBgColor(Color(0, 0, 0, 255));
	m_TextPanel->SetFgColor(Color(0, 255, 0, 255));
	m_TextPanel->AddActionSignalTarget( this );
	m_TextPanel->SetPos( 0, 0 );
	m_TextPanel->SetSize( vgui::scheme()->GetProportionalScaledValue(640), vgui::scheme()->GetProportionalScaledValue(468) );
	m_TextPanel->SetVerticalScrollbar( false );

	SetScheme(vgui::scheme()->LoadSchemeFromFile("resource/ComputerScheme.res", "ComputerScheme"));
	LoadControlSettings("resource/UI/vampireComputer.res");
	vgui::ivgui()->AddTickSignal( GetVPanel(), 100 );

	m_Parser = new C_ComputerParser( m_TextPanel, m_pTime );
	pComputerFunc = this->m_Parser->GetComputerFunc();

	DevMsg("vampireComputer has been constructed\n");
}

ConVar cl_computer_window("cl_showvampireComputer", "1", FCVAR_CLIENTDLL, "Sets the state of vampireComputer <state>");

//Class: CVampireComputerInterface Class. Used for construction.
class CVampireComputerInterface : public IVampireComputer
{
private:
	CVampireComputer *vampireComputer;
public:
	CVampireComputerInterface()
	{
		vampireComputer = NULL;
	}
	void Create(vgui::VPANEL parent)
	{
		vampireComputer = new CVampireComputer(parent);
	}
	void Destroy()
	{
		if (vampireComputer)
		{
			vampireComputer->SetParent( (vgui::Panel *)NULL);
			delete vampireComputer;
		}
	}
	void Activate( void )
	{
		if ( vampireComputer )
		{
			// Parse the file again
			vampireComputer->refreshScript();
			vampireComputer->Activate();
		}
	}
	void Deactivate( void )
	{
		if (vampireComputer) 
		{
			cl_computer_window.SetValue(0);
			vampireComputer->Deactivate();
		}
	}
};
static CVampireComputerInterface g_vampireComputer;
IVampireComputer* vampireComputer = (IVampireComputer*)&g_vampireComputer;

CON_COMMAND(ToggleComputer, "Toggles the computerwindow on or off")
{
	cl_computer_window.SetValue(!cl_computer_window.GetBool());
	vampireComputer->Activate();
};

void CVampireComputer::OnTick()
{
	BaseClass::OnTick();
	SetVisible(cl_computer_window.GetBool()); //cl_computer_window, 1 by default
}

void CVampireComputer::OnCommand(const char* pcCommand)
{
	if(!Q_stricmp(pcCommand, "enter"))
	{
		char *tmp = new char[m_pTime->GetTextLength()+1];
		m_pTime->GetText(tmp,m_pTime->GetTextLength()+1);
		DevMsg("Received: %s \n", tmp);
		delete [] tmp;
		tmp = 0;
		m_pTime->SetText("");
		//cl_showvampireComputer.SetValue(0);
	}
}

void CVampireComputer::OnKeyCodePressed( KeyCode code )
{
	DevMsg("KeyCodePressed: %i\n", code);
	switch(code)
	{
		case KEY_ENTER:
			if( m_pTime->HasFocus() )
			{
				char *tmp = new char[m_pTime->GetTextLength()+1];
				m_pTime->GetText( tmp, m_pTime->GetTextLength()+1 );
				DevMsg("Received: %s \n", tmp);
				//m_TextPanel->SetText(tmp);

				//change this to a switch statement?
				if( pComputerFunc->GetOnlyEnter() )
					pComputerFunc->Enter();
				else if( pComputerFunc->IsPasswordReq() )
					pComputerFunc->HandlePassword(tmp);
				else
					pComputerFunc->HandleCmd(tmp);
				
				delete[] tmp;
				tmp = 0;
				//m_pTime->SetText("");
			}
			break;

		case KEY_ESCAPE:
			cl_computer_window.SetValue(false);
			vampireComputer->Activate();
			break;
	}
	BaseClass::OnKeyCodePressed( code );
}

/*void CVampireComputer::OnCursorMoved( int x, int y )
{
	if( x <= 100 && y <= 100 ) 
	{
		DevMsg("It works! lol\n");
	}
}*/

/*void CVampireComputer::OnMessage( KeyValues* data )
{
	DevMsg("GOt som einfofofofin\n");
}*/