#ifndef C_COMPUTER_PARSER_H
#define C_COMPUTER_PARSER_H

#include <iostream>
#include <sstream>
#include <string>
#include <fstream>
#include <vector>
#include "C_ComputerStruct.h"
#include "C_ComputerFunc.h"
#include "KeyValues.h"
#include "FileSystem.h"
#include <vgui_controls/RichText.h> 
#include <vgui_controls/TextEntry.h>
using namespace vgui;

//#define BASEPATH "../../sourcemods/bloodlinesresurgence/"

class C_ComputerFunc;

struct stLogonScreen
{
	std::string strLines;
};

class C_ComputerParser
{
public:
	C_ComputerFunc* GetComputerFunc();
	C_ComputerParser(vgui::RichText *vguiTextEntry, vgui::TextEntry *vguiCmd);
	C_ComputerParser();
	~C_ComputerParser();
	void Parse();
	void Say(const char *what);
private:
	vgui::RichText *m_vguiRichText;
	C_ComputerFunc *pComputer;
protected:
};

#endif