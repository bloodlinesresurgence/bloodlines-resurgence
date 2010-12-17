#include "cbase.h"
#include "vamp_player.h"
#include "server_class.h"
#include "filesystem.h"

LINK_ENTITY_TO_CLASS( player, CVamp_Player );

BEGIN_DATADESC(CVamp_Player)
	// TODO
END_DATADESC()

CVamp_Player::CVamp_Player()
{
	// FIXME: Set these to proper defaults
	m_iMoney = 0;
	m_iHumanity = 0;
	m_iMasquerade = 0;
	m_iBlood = 0;
	m_iExp = 0;
}

/*
IMPLEMENT_SERVERCLASS_ST( CVamp_Player, DT_VampPlayer )
	SendPropInt(	SENDINFO( m_nMyInteger ), 8, SPROP_UNSIGNED ),
END_SEND_TABLE()
*/
void CVamp_Player::Spawn(void)
{
	Msg("CVamp_Player Spawn()\n");
	BaseClass::Spawn();
}

void CVamp_Player::WorldMap(int worldmap_state)
{
	// WRITEME
}

void CVamp_Player::SewerMap(int worldmap_state)
{
	// WRITEME
}

void CVamp_Player::BumpStat(std::string stat, int val)
{
	// WRITEME
}

void CVamp_Player::GiveItem(std::string item_name)
{
	int idx;
	VampItem * item = new VampItem();
	item->name = item_name;
	item->amount = 1;

	idx = m_vInventory.Find(item);
	if (idx == m_vInventory.InvalidIndex()) {
		m_vInventory.AddToTail(item);
	} else {
		m_vInventory[idx]->amount += 1;
		delete item;
	}
	// FIXME: We need to remember to delete all the items 
	// in the inventory on exit or when we drop an item, or revamp the inventory
}

void CVamp_Player::giveAmmo(std::string weapon_name, int amt)
{
	// FIXME: there exists a GiveAmmo function already. Use that?
}

void CVamp_Player::HumanityAdd(int h)
{
	// FIXME: Set MAX Humanity level
	if (m_iHumanity + h < 0) {
		m_iHumanity = 0;
	} else {
		m_iHumanity += h;
	}
	
}

void CVamp_Player::Bloodgain(int b)
{
	// FIXME: Set MAX Blood level
	if (m_iBlood + b < 0) {
		m_iBlood = 0;
	} else {
		m_iBlood += b;
	}
}

void CVamp_Player::Bloodloss(int b)
{
	Bloodgain(-b);
}


void CVamp_Player::MoneyAdd(int m)
{
	if (m_iMoney + m < 0) {
		m_iMoney = 0;
	} else {
		m_iMoney += m;
	}
}

void CVamp_Player::BloodHeal()
{
	if (m_iBlood > 0) {
		m_iBlood -= 1;
		// FIXME: Increase health
	}
}

void CVamp_Player::ChangeMasqueradeLevel(int m)
{
	// FIXME: gamePath should be a global
	char gamePath[256];
	engine->GetGameDir( gamePath, 256 );
	Q_StripTrailingSlash( gamePath );
	Msg("gamePath: %s\n", gamePath);
	Msg("Blah: %s\n", UTIL_VarArgs("%s/vdata/system/feats.txt", gamePath));
	KeyValues *kv = new KeyValues("feats.txt");
	KeyValues::AutoDelete autodelete_kv(kv);
	// Why does this say mod?
	if (! kv->LoadFromFile(filesystem, UTIL_VarArgs("%s/vdata/system/feats.txt", gamePath), "MOD")) {
		Warning("LoadFromFile failed!");
		return;
	}
	Msg("Key Names:\n");
	KeyValues *tmp = kv->GetFirstSubKey();
	for (KeyValues *sub = tmp->GetFirstSubKey(); sub; sub = sub->GetNextKey()) {
		Msg("\t '%s' '%s'\n", sub->GetName(), sub->GetString());
	}
	if (m_iMasquerade + m < 0) {
		m_iMasquerade = 0;
	} else {
		m_iMasquerade += m;
	}
}

int CVamp_Player::CalcFeat(std::string feat)
{
	// WRITEME
	return 0;
}

void CVamp_Player::DebugDump()
{
	Msg("CVamp_Player Health: %d\n", m_iHealth);
	Msg("\tmoney: %d\n", m_iMoney);
	Msg("\thumanity: %d\n", m_iHumanity);
	Msg("\tMasquerade violations: %d\n", m_iMasquerade);
	Msg("\tblood points: %d\n", m_iBlood);
	Msg("\tunused XP: %d\n", m_iExp);
}

CVamp_Player::~CVamp_Player()
{
	m_vInventory.PurgeAndDeleteElements();
}

CVamp_Player * FindPlayer()
{
	return (CVamp_Player *) UTIL_GetLocalPlayer();
}