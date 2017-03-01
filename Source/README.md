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
