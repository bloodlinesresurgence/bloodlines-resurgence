#ifndef C_COMPUTER_FUNC_H
#define C_COMPUTER_FUNC_H

#include <vector>
#include <iostream>
#include <vgui_controls/RichText.h>
#include <vgui_controls/TextEntry.h>
#include "C_ComputerStruct.h"
#include "C_ComputerParser.h"

//#define BASEPATH "../../sourcemods/bloodlinesresurgence/"

class C_ComputerFunc
{
public:
	void AddSubDir(stSubDir *SubDir); //takes a subdir, should be populated with functions aswell
	void Show();
	void ShowMainMenu();
	void ShowSubDir(int nSubDir);
	void ShowPasswordPrompt(int nSubDir, bool bRecursed);
	void ShowHelp();
	void ShowList();
	void ShowError(const char *szCmd);
	void HandleCmd(const char *szCmd);
	void HandlePassword(const char *szCmd);
	void Say(const char *szSay);
	void AddLogon(std::string strLogon);
	void Enter();
	bool GetOnlyEnter();
	bool IsPasswordReq();
	C_ComputerFunc* GetClass();
	C_ComputerFunc(vgui::RichText *vguiText, std::string strLogon);
	C_ComputerFunc(vgui::RichText *vguiText, vgui::TextEntry *vguiCmd);
	C_ComputerFunc();
	~C_ComputerFunc();
private:
	bool m_bOnlyEnter; //only enter key allowed
	bool m_bFallback; //if we have a menu to fall back to
	bool m_bPasswordReq; //is a password required?
	int m_nCurrentSubDir; //current directory we are in, -1 means mainmenu
	int m_nCurrentFunc; //current function we are showing
	int m_nPasswordReq; //what directory requires the password?
	stComputerText m_ComputerText;
	vgui::RichText *m_vguiText;
	vgui::TextEntry *m_vguiCmd;
	std::string m_strLogon;
	std::vector<stSubDir*> m_SubDir;
protected:
};

#endif