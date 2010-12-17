///////////// Copyright © 2009 LodleNet. All rights reserved. /////////////
//
//   Project     : Server
//   File        : ge_pymodules.cpp
//   Description :
//      [TODO: Write the purpose of ge_pymodules.cpp.]
//
//   Created On: 9/1/2009 10:19:52 PM
//   Created By: Mark Chandler <mailto:mark@moddb.com>
////////////////////////////////////////////////////////////////////////////

#include "cbase.h"

#include "baseentity.h"
#include "vamp_player.h"
#include <boost/python.hpp>
namespace bp = boost::python;

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

#define REG( Name )	extern void init##Name(); PyImport_AppendInittab( #Name , & init##Name );	

void pyMsg(const char* msg)
{
	Msg(msg);
}

void pyWarning(const char* msg)
{
	Warning(msg);
}

BOOST_PYTHON_MODULE(HLUtil)
{
	bp::def("Msg", pyMsg);
	bp::def("Warning", pyWarning);
}

BOOST_PYTHON_MODULE(Vampire)
{
	bp::def("FindPlayer", FindPlayer, bp::return_value_policy<bp::reference_existing_object>());
	bp::class_<CVamp_Player, boost::noncopyable>("CVamp_Player", bp::no_init)
		.def("WorldMap", &CVamp_Player::WorldMap)
		.def("SewerMap", &CVamp_Player::SewerMap)
		.def("BumpStat", &CVamp_Player::BumpStat)
		.def("GiveItem", &CVamp_Player::GiveItem)
		.def("giveAmmo", &CVamp_Player::giveAmmo)
		.def("HumanityAdd", &CVamp_Player::HumanityAdd)
		.def("Bloodgain", &CVamp_Player::Bloodgain)
		.def("Bloodloss", &CVamp_Player::Bloodloss)
		.def("MoneyAdd", &CVamp_Player::MoneyAdd)
		.def("BloodHeal", &CVamp_Player::BloodHeal)
		.def("ChangeMasqueradeLevel", &CVamp_Player::ChangeMasqueradeLevel)
		.def("CalcFeat", &CVamp_Player::CalcFeat)
		.def("DebugDump", &CVamp_Player::DebugDump)
		;
}

extern "C"
{

void RegisterPythonModules()
{
	REG( HLUtil );
	REG( Vampire );
}

}