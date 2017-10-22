StateFunding
------------

forked from iamchairs/StateFunding

Local modifications by TheDog:
- compile for KSP 1.2
- rewrite persistence (save/load) to use KSP standard persistence via scenariomodule

Adoption by Linuxgurugamer
0.6.0
	Initial release

0.6.0.1
	Added checks for null when getting the game instanceo

0.6.0.2
	Added new goverment which was posted in the old thread

0.6.1
	Fixed segfaults caused by planet packs which changed the name of the sun

0.6.2
	Fixed check for satellites not being in orbit around the sun, replaced hard-coded name
	Added Relay type to check for satellites

0.6.3
	Now using Planetarium.fetch.Sun to identify the sun
	Added Kerbal's republic of Kerna, thanks @Maxzhao1999

0.6.4
	Added SpaceK contributed by @Rock3tman_
	Added budgetPeriodsPerYear, to allow for more than (or less than) 4 periods/year
	Now using Planetarium.fetch.Home.orbit.period to get the correct orbial period of the homeworld.
	Added settings page for customization of budgetPeriodsPerYear and Enabled flag
	Added code to prevent mod from running if not CAREER
	Added option to stop warp at the start of a new budget period
	Added option to stop warp at end of year
	Fixed typo in new review available message
	Fixed position of new review available message to not cover tolbar
	Changed the budgetPeriodsPerYear on some governments to other than 4

0.6.5
	Added new government, Kikkstarter Krowd Funding Platform, contributed by @MysterySloth
	Corrected spelling of Astroid to Asteroid
	Fixed poModifier/scModifier, thanks @PhilM-47 

0.6.6
	Added KLA thanks to @rasta013

0.6.7
	Fixed crashes being counted 2x

0.6.8
	Added generator aliases, thanks @philm-47
	Added code to deal with Fuel Cells
0.6.8.1
	Added alias for Antenna, to support Kerbalism

0.6.9
	Updated for 1.3

0.6.9.1
	Added alias for KopernicusSolarPanels

0.6.10
	Added Kerbal Kooperative Union, thanks @mrcarrot
	Reduced height of buttons in gov sel window from 30 to 25

0.6.11
	Updated for KSP 1.3.1

0.6.12
	Added new government,  North Va'ar
	Updated deploy and build scripts