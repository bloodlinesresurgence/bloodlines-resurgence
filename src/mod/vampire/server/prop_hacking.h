#pragma once
#include "cbase.h"

class CPropHacking : public CBaseAnimating {
public:
	DECLARE_CLASS(CPropHacking, CBaseAnimating);
	DECLARE_DATADESC();

	CPropHacking () {
		model = "models/scenery/furniture/computer/computer_monitor.mdl";
	}

	void Precache (void);
	void Spawn (void);
	
	void Trigger(int trigger);
	void Use (CBaseEntity *pActivator, CBaseEntity *pCaller, USE_TYPE useType, float value);

private:
	char* hack_file;
	char* model;

	// Outputs
	// It looks like code will define an output to call at specified events
	// @TODO Implement the event triggering
	COutputEvent OnTrigger0;
	COutputEvent OnTrigger1;
	COutputEvent OnTrigger2;
	COutputEvent OnTrigger3;
	COutputEvent OnTrigger4;
	COutputEvent OnTrigger5;
	COutputEvent OnTrigger6;
	COutputEvent OnTrigger7;
	COutputEvent OnTrigger8;
	COutputEvent OnTrigger9;

	// Not implemented
	int textrows;
	int textcolumns;
	int colorscheme;
	char *target_name;
	int diceroll;
	bool StartHidden;
	int difficulty;
	bool start_enabled;
	bool global_email;
	float ss_start;
	float ss_delay;
	char *soundgroup;
	int skilltype;
};

extern CPropHacking* prop_hacking_callee;