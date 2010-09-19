#ifndef C_COMPUTER_STRUCT_H
#define C_COMPUTER_STRUCT_H

// http://en.wikipedia.org/wiki/Hungarian_notation says st for structure
struct stComputerText
{
	std::string strHelp;
	std::string strInvalidCmd;
};

struct stComputerFunc
{
	/*		
	taken from hack_example.txt
	"name"		"Unlock"			// this is what the player must type in to trigger this function. (16 characters)
	"description"	"Unlock Safe"			// this is a description of this function  (128 characters)
	"runtext"	"Safe doors unlocked."		// this text is printed when the player triggers this function.  (128 characters)
	"trigger"	"0"				// fires entity output 0 when this command is run
	"runscript"	"G.Safe_Locked = 0"
	*/
	char szCmdName[16]; //this is what the player must type in to trigger this function. 16
	char szDesc[128]; //description of the func, 128
	char szRunText[128]; //text to show when triggered, 128
	int nTrigger; //trigger thingie, don't know bout this yet
	//char szRunScript[256]; //when python is implemented
};

struct stSubDir
{
	/*
	taken from hack_example.txt
	"name"			"Safe"				// This is what is displayed as the name of this menu, and what the player must type to enter the menu (128 characters)
	"password"		"griff"				// password required to run commands in this menu (16 characters)
	"description"		"Safe Security Controls"	// This is printed each time the user enters this menu (128 characters)
	*/
	char szName[128]; //displayed as name and what the user must type to enter the menu
	char szPass[16]; //password required to run commands in this submenu
	char szDesc[128]; //this is printed each time the user enters this menu
	bool bEnteredPass; //if the password has already been entered
	std::vector<stComputerFunc*> vecFunctions;
};

#endif