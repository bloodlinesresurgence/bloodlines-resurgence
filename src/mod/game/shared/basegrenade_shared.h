//========= Copyright � 1996-2005, Valve Corporation, All rights reserved. ============//
//
// Purpose: 
//
// $NoKeywords: $
//=============================================================================//

#ifndef BASEGRENADE_SHARED_H
#define BASEGRENADE_SHARED_H
#ifdef _WIN32
#pragma once
#endif

#if defined( CLIENT_DLL )

#define CBaseGrenade C_BaseGrenade

#include "c_basecombatcharacter.h"

#else

#include "basecombatcharacter.h"
#include "player_pickup.h"

#endif

#define BASEGRENADE_EXPLOSION_VOLUME	1024

class CTakeDamageInfo;

//Tony; Compromise! in episodic single player, inherit CBaseCombatCharacter for the barnacle interaction, otherwise this will never get called.
class CBaseGrenade : 
	#if defined( HL2_EPISODIC )
		public CBaseCombatCharacter
	#else
		public CBaseAnimating
	#endif
	#if defined( GAME_DLL )
		, public CDefaultPlayerPickupVPhysics
	#endif
{		//Tony; the ugliest class definition ever, but it saves characters, or something. Should I be shot for this?
	DECLARE_CLASS( CBaseGrenade, CBaseAnimating );
public:

	CBaseGrenade(void);
	~CBaseGrenade(void);

	DECLARE_PREDICTABLE();
	DECLARE_NETWORKCLASS();


#if !defined( CLIENT_DLL )
	DECLARE_DATADESC();
#endif

	virtual void		Precache( void );

	virtual void		Explode( trace_t *pTrace, int bitsDamageType );
	void				Smoke( void );

	void				BounceTouch( CBaseEntity *pOther );
	void				SlideTouch( CBaseEntity *pOther );
	void				ExplodeTouch( CBaseEntity *pOther );
	void				DangerSoundThink( void );
	void				PreDetonate( void );
	virtual void		Detonate( void );
	void				DetonateUse( CBaseEntity *pActivator, CBaseEntity *pCaller, USE_TYPE useType, float value );
	void				TumbleThink( void );

	virtual Vector		GetBlastForce() { return vec3_origin; }

	virtual void		BounceSound( void );
	virtual int			BloodColor( void ) { return DONT_BLEED; }
	virtual void		Event_Killed( const CTakeDamageInfo &info );

	virtual float		GetShakeAmplitude( void ) { return 25.0; }
	virtual float		GetShakeRadius( void ) { return 750.0; }

	// Damage accessors.
	virtual float GetDamage()
	{
		return m_flDamage;
	}
	virtual float GetDamageRadius()
	{
		return m_DmgRadius;
	}

	virtual void SetDamage(float flDamage)
	{
		m_flDamage = flDamage;
	}

	virtual void SetDamageRadius(float flDamageRadius)
	{
		m_DmgRadius = flDamageRadius;
	}

	// Bounce sound accessors.
	void SetBounceSound( const char *pszBounceSound ) 
	{
		m_iszBounceSound = MAKE_STRING( pszBounceSound );
	}

	CBaseCombatCharacter *GetThrower( void );
	void				  SetThrower( CBaseCombatCharacter *pThrower );
	CBaseEntity *GetOriginalThrower() { return m_hOriginalThrower; }

	// added for entity info so that certain classes can override this, without screwing up the normal GetOwnerEntity()
	virtual CBaseEntity	*GetTrueOwnerEntity()
	{
		return dynamic_cast<CBaseEntity *>( GetThrower() );
	}

#if !defined( CLIENT_DLL )
	// Allow +USE pickup
	int ObjectCaps() 
	{ 
		return (BaseClass::ObjectCaps() | FCAP_IMPULSE_USE | FCAP_USE_IN_RADIUS);
	}

	void				Use( CBaseEntity *pActivator, CBaseEntity *pCaller, USE_TYPE useType, float value );
#endif

public:
	IMPLEMENT_NETWORK_VAR_FOR_DERIVED( m_vecVelocity );
	IMPLEMENT_NETWORK_VAR_FOR_DERIVED( m_fFlags );
	
	bool				m_bHasWarnedAI;				// whether or not this grenade has issued its DANGER sound to the world sound list yet.
	CNetworkVar( bool, m_bIsLive );					// Is this grenade live, or can it be picked up?
	CNetworkVar( float, m_DmgRadius );				// How far do I do damage?
	CNetworkVar( float, m_flNextAttack );			// Added into grenade itself now that it's no longer CBaseCombatCharacter
	float				m_flDetonateTime;			// Time at which to detonate.
	float				m_flWarnAITime;				// Time at which to warn the AI

protected:

	CNetworkVar( float, m_flDamage );		// Damage to inflict.
	string_t m_iszBounceSound;	// The sound to make on bouncing.  If not NULL, overrides the BounceSound() function.

private:
	CNetworkHandle( CBaseEntity, m_hThrower );					// Who threw this grenade
	EHANDLE			m_hOriginalThrower;							// Who was the original thrower of this grenade

	CBaseGrenade( const CBaseGrenade & ); // not defined, not accessible

};

#endif // BASEGRENADE_SHARED_H
