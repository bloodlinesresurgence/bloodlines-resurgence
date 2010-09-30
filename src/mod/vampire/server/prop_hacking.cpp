/**
 * prop_hacking
 *
 * A model entity that can be 'used' to bring up the computer interface
 */

#include "cbase.h"
#include "EventQueue.h"

#include <string>

#include "prop_hacking.h"
 
// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

CPropHacking* prop_hacking_callee;

LINK_ENTITY_TO_CLASS(prop_hacking, CPropHacking);

BEGIN_DATADESC(CPropHacking)
	// Not used yet
	DEFINE_FIELD(textrows, FIELD_INTEGER),		// ?? Scaling?
	DEFINE_FIELD(textcolumns, FIELD_INTEGER),	// ?? Scaling?
	DEFINE_FIELD(colorscheme, FIELD_INTEGER),	// ?? I don't recall any other color schemes
	DEFINE_FIELD(target_name, FIELD_STRING),	// ?? 
	DEFINE_FIELD(diceroll, FIELD_INTEGER),		// ?? No idea
	DEFINE_FIELD(StartHidden, FIELD_BOOLEAN),	// ?? Don't display the model yet
	DEFINE_FIELD(difficulty, FIELD_INTEGER),	// ?? Hack difficulty
	DEFINE_FIELD(start_enabled, FIELD_BOOLEAN),	// Whether we can use it straight away
	DEFINE_FIELD(global_email, FIELD_BOOLEAN),	// ?? I suppose this would show email from everywhere
	DEFINE_FIELD(ss_start, FIELD_FLOAT),	// Screensaver start
	DEFINE_FIELD(ss_delay, FIELD_FLOAT),	// Screensaver delay
	DEFINE_FIELD(soundgroup, FIELD_STRING),	// ??
	DEFINE_FIELD(skilltype, FIELD_INTEGER),	// ?? I suppose it links into skills

	// Implemented
	DEFINE_FIELD(hack_file, FIELD_STRING),
	DEFINE_FIELD(model, FIELD_MODELNAME),
	DEFINE_OUTPUT(OnTrigger0, "OnTrigger0"),
	DEFINE_OUTPUT(OnTrigger1, "OnTrigger1"),
	DEFINE_OUTPUT(OnTrigger2, "OnTrigger2"),
	DEFINE_OUTPUT(OnTrigger3, "OnTrigger3"),
	DEFINE_OUTPUT(OnTrigger4, "OnTrigger4"),
	DEFINE_OUTPUT(OnTrigger5, "OnTrigger5"),
	DEFINE_OUTPUT(OnTrigger6, "OnTrigger6"),
	DEFINE_OUTPUT(OnTrigger7, "OnTrigger7"),
	DEFINE_OUTPUT(OnTrigger8, "OnTrigger8"),
	DEFINE_OUTPUT(OnTrigger9, "OnTrigger9"),
	DEFINE_USEFUNC(Use),
END_DATADESC()

//-----------------------------------------------------------------------------
// Purpose: Precache the model used
//-----------------------------------------------------------------------------
void CPropHacking::Precache(void)
{
	PrecacheModel(model);

	BaseClass::Precache();
}

//-----------------------------------------------------------------------------
// Purpose: Spawn the entity
//-----------------------------------------------------------------------------
void CPropHacking::Spawn(void)
{
	Precache();

	SetModel(model);
	SetSolid(SOLID_BBOX);
	UTIL_SetSize(this, -Vector(20, 20, 20), Vector(20,20,20));
}

//-----------------------------------------------------------------------------
// Purpose: Used by computer interpreter to trigger output
//-----------------------------------------------------------------------------
// This is ugly, need a cleaner way of doing this
void CPropHacking::Trigger (int trigger) {
	// Is there an easier way to do this?
	COutputEvent *t;
	switch(trigger) {
		case 0:
			t = &OnTrigger0;
			break;
		case 1:
			t = &OnTrigger1;
			break;
		case 2:
			t = &OnTrigger2;
			break;
		case 3:
			t = &OnTrigger3;
			break;
		case 4:
			t = &OnTrigger4;
			break;
		case 5:
			t = &OnTrigger5;
			break;
		case 6:
			t = &OnTrigger6;
			break;
		case 7:
			t = &OnTrigger7;
			break;
		case 8:
			t = &OnTrigger8;
			break;
		case 9:
			t = &OnTrigger9;
			break;
		default:
			// Not valid
			DevMsg("Invalid trigger index");
			return;
	};
	t->FireOutput(this, this);
}

//-----------------------------------------------------------------------------
// Purpose: Occurs when the entity is 'use'd
//-----------------------------------------------------------------------------
void CPropHacking::Use (CBaseEntity *pActivator, CBaseEntity *pCaller, USE_TYPE useType, float value)
{
	// if it's not a player, ignore
	if ( !pActivator || !pActivator->IsPlayer() )
		return;

	prop_hacking_callee = this;
	//cl_computer_script.SetValue(this->hack_file);
	//cl_computer_window.SetValue(1);
	std::string script = "cl_computer_script ";
	script.append(this->hack_file);
	std::string show = "cl_showvampireComputer 1";

	// Run it as if from the client
	edict_t *e = engine->PEntityOfEntIndex(pActivator->entindex());

	engine->ClientCommand(e, script.c_str());
	engine->ClientCommand(e, show.c_str());
}