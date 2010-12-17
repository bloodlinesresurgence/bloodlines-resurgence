#ifndef VAMP_PLAYER_H
#define VAMP_PLAYER_H

#include "cbase.h"
#include "hl2_player.h"
#include <string>

// FIXME: This is just a prototype
struct VampItem {
	std::string		name;	// name of the item "item_...."
	unsigned int	amount;	// amount of it that you have or ammo ?
	bool operator == (const VampItem &b) {
		return (b.name == name);
	}
	bool operator < (const VampItem &b) {
		return (b.name < name);
	}
};

// TODO: Do we want to inherit from CHL2_Player or CBasePlayer?
class CVamp_Player : public CHL2_Player
{
public:
	DECLARE_CLASS( CVamp_Player, CHL2_Player );
	CVamp_Player();
	~CVamp_Player( void );
	// TODO: I don't quite understand the edicts?
	static CVamp_Player *CreatePlayer( const char *className, edict_t *ed )
	{
		Msg("CVamp_Player CreatePlayer\n");
		CVamp_Player::s_PlayerEdict = ed;
		return (CVamp_Player*)CreateEntityByName( className );
	}

//	DECLARE_SERVERCLASS(); // TODO: I don't think we need to declare serverclass but not sure.
	DECLARE_DATADESC();
//	CNetworkVar( int, m_nMyInteger ); // integer

	virtual void		Spawn(void);

	void	WorldMap(int worldmap_state);				// Opens the WorldMap city chooser
	void	SewerMap(int worldmap_state);				// Same as WorldMap except for Nosferatu
	void	BumpStat(std::string stat, int val);		// Raise a player stat
	void	GiveItem(std::string item_name);			// Give player specified item
	void	giveAmmo(std::string weapon_name, int amt);	// Replenish ammo for specified weapon
	void	HumanityAdd(int h);							// Raise or lower humanity level
	void	Bloodgain(int b);							// Raise blood level by 'b' units
	void	Bloodloss(int b);							// Lower blood level by 'b' units
	void	MoneyAdd(int m);							// Add Money to player wallet
	void	BloodHeal();								// Perform a blood heal
	void	ChangeMasqueradeLevel(int m);				// Raise or lower masquerade level
	int		CalcFeat(std::string feat);					// Calculate value of specified feat
	void	DebugDump();

private:
	CUtlVector<VampItem*> m_vInventory;	// Vector of items that you have. 
	int m_iMoney;						// Amount of money player has
	int m_iHumanity;					// Humanity level
	int m_iMasquerade;					// Number of times player has violated the masquerade
	int m_iBlood;						// Blood level
	int m_iExp;							// Unused experience that is available
	// TODO:
	// stats, frenzy?, 
};

CVamp_Player * FindPlayer(void);	

#endif