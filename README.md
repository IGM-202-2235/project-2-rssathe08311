# Project BOIDS

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Reva Sathe
-   Section: 02

## Simulation Design

This simulation will be based off of a post-apocolyptic zombie-like comic I made during high school.  This project features at the moment 2 agent types. A superpowered human and shades which are husk-like creatures that will go after humans until humans start to attack. This will be a survival simulation, whre players will be able to attempt to aid humans be delivering resources to aid their survival. 

### Controls

-   _List all of the actions the player can have in your simulation_
    -   _Include how to preform each action ( keyboard, mouse, UI Input )_
    -   _Include what impact an action has in the simulation ( if is could be unclear )_


## Agent 1 - Human

A super powered human adept at fighting off Shades in a post-apocolyptic world. Though if the human cannot fight back they must do their best to run away and survive.

### State 1: Run/Survive 

**Objective:** If the group of shades attacking a human are too big or the human cannot attack the human will try to evade hordes of Shades.

#### Steering Behaviors

- _List all behaviors used by this state_
   - _If behavior has input data list it here_
   - _eg, Flee - nearest Agent2_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
- Evade
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   - _eg, When this agent gets within range of Agent2_
   - _eg, When this agent has reached target of State2_
   
### State 2: Fight

**Objective:** If humans are able to attack they will actively fight back agains shades. 

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
   
#### State Transistions

- _List all the ways this agent can transition to this state_

## Agent 2 - Shades
Husk like creatures that are a result of the nuclear apocalypse, they are out to hunt any humans they come across unless they are being attacked. Which in that case they do their best to run away.  

### State 1: Search/Pursue

**Objective:** While in pursuit of humans Shades will look for them in groups in order to try and kill them. They will move around in a flock-like manner. 

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   
### State 2: Run

**Objective:** When being attacked by humans the flock-like behavior will become less of a priority as the Shades attempt to escape.

#### Steering Behaviors

- _List all behaviors used by this state_
- Obstacles - _List all obstacle types this state avoids_
- Seperation - _List all agents this state seperates from_
   
#### State Transistions

- _List all the ways this agent can transition to this state_

## Sources

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_
-   The uncompleted webcomic this is based off of: [https://www.webtoons.com/en/canvas/unknown-tomorrow/list?title_no=379290](url)

## Make it Your Own
This project is entirely based off of Unknown Tomorrow, a webcomic I created in highschool. 
- I will be drawing all the art assets for sprites, backgrounds, and obstacles.
- I may potentially add in a third agent that will be another human type agent that is hunting down all other agent types including other humans. 

## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

