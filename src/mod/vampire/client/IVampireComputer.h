// IVampireComputer.h
class IVampireComputer
{
public:
	virtual void		Create( vgui::VPANEL parent ) = 0;
	virtual void		Destroy( void ) = 0;
	virtual void		Activate( void ) = 0;
	virtual void		Deactivate( void ) = 0;
};
 
extern IVampireComputer* vampireComputer;