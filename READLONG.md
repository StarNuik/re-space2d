# Challenge design
Finish in a week
* 1-2 days of programming
* 2-3 days of content setup

PS:
* 1 day of GUI drafting
* 1 day of AttackPattern preview :^)



# Game design


## General direction
An arcade twin-stick shooter


## Design't
Features that are too fat to do in 2-3 hours (code &| content)

* Multiple scenes
* Saved PC progression
* Multiple bosses


## Designest
Features that are dirt cheap to implement (in either code | content)
* SFX
* BGM
* Screen effects (on events like PC health, NPC destruction etc)
* VFX using sprites
* Trails
* Background (parallax-ed bgs are cheap too)
* Physics collisions (these don't really fit into the gameplay part. Mb I can use them for debris?)
* Lighting

PS:
* Bullet color (depending on damage)


## Design
This is data design intertwined with flow design. This was the easiest way for me to put it on (digital) paper
* Bg (the easist way is to make it a global system)
	* The bg can change depending on conditions like: boss present, pc low health, etc
* Splash / start screen
	* Leaderboards (for the arcade feel)
	* Made by: me
	* Press X to start
* On start
	* GUI
	* A controllable pc
		* WASD to move
		* Arrows to shoot
	* Stages (aka levels, setting up SOs with linked enemies is a cheap way to create content)
* GUI
	* Health
	* Stage
	* Score
	* ¿Upgrades?
* A stage
	* Settings
		* Enemies to spawn during the stage (type + count)
		* Upgrades to spawn (key + value)
		* That's it
	* Spawns enemies
		* Different algos for enemy spawning might be a cheap way to add some diversity. But they also may go totally unnoticed by the Player
	* Spawns upgrades
		* Preferrably near the middle of the stage.
	* Waits for the enemies and upgrades to die
	* Next stage
* An enemy - I see it as a rather functional entity
	* Moves in a pattern (movement pattern SO)
	* Shoots in a pattern (attack pattern SO)
	* Has health
	* Dies
		* From bullets (adds score)
		* Goes off-screen
* A boss : enemy
	* Stages - the only addition to the enemy
		* Quick thought - an enemy can be just a 1 stage boss
	* Doesn't die off-screen
* An upgrade
	* Moves in a pattern (no need to overcomplicate upgrades, a line will be enough)
	* Dies
		* Gets eaten by the PC (adds score)
		* Goes off-screen
	* Upgrades
		* Health
		* Move speed
		* Bullet damage
		* Attack pattern
* The Player's death
	* Show the score (and the placement on the LB)
		* Choose a name
	* Press A to restart, press B to go to the start

PS:
* A combo system

## Data design
Since the project isn't huge, I might as well quickly map this out

### Static
* Basic enemy SO
	* Total HP
	* Stage info
* Boss enemy SO
	* Total HP
	* Stages
		* HP percent
		* Stage info
* Enemy stage info
	* Bullet damage
	* Movement speed
	* Movement pattern SO
	* Attack pattern SO
* PC upgrade
	* Movement speed
	* Movement pattern SO
	* Upgrade SO
* Level stage SO
	* Spawn pattern SO
	* Enemies
		* Enemy type SO - count
	* Upgrades
		* Upgrade type SO - count
* Level progression SO
	* Level stages
	* Infinite mode stages
* PC So
	* Upgradeable box
		* Health
		* Move speed
		* Bullet damage
		* Attack pattern index
	* Starting stats
		* Upgradeable box
	* Max stats
		* Upgradeable box
	* Attack patterns SO

### Saved
* Leaderboard
	* Score - name Saved

### Runtime
* Game
	* Screen (start, gamemode, end)
	* Current stage
		* Enemies left
		* Upgrades left
* PC
	* Input
	* Position
	* Health
	* Upgrades
		* PCBox
	* Score
* Enemies
	* Enemy[]
		* Position
		* Health
		* (if Boss) current stage

### Runtime services
(not sure if I can accurately identify these)
* Game service
	* ToState(start, gamemode, end)
	* Update() - stages
* PC service
	* Input()
	* RegisterDamage()
	* RegisterUpgrade()
	* Update() - self
* Enemies service
	* Create()
	* RegisterDamage()
	* Update() - all enemies



# Actually doing stuff

## Time spent:
* Day 1: planning, gui layouts, basic gui system
* Day 2: PC movement, rotation, AttackPattern asset preview (a massive tangent, yey)
* Day 3: bullets (and mvc)
* Day 4: un-mvc-ing bullets, removing off-screne bullets, basics of enemy movement
* Day 5: enemy movement, enemy rotation
* Day 6:

## A week has passed
Reflecting on my architecture choice, I decided that:
MVC is great, but it doesnt work well with Unity's components, thus:
It might be smart to use mvc for UI and business logic, and components for the gameplay
I'll need some sort of a connector between the business side and the gameplay side, but events should be able to handle *that* much

## Overqualification
I just realized that AttackPattern could be replaced with a prefab with a few transforms and tags on them.
It seems I'm so "good" at programming that I forgot and forgone the basics. I will end the project, as it is no longer a "game dev" challenge and more of a "make cool, clean and reusable programatic systems" challenge.