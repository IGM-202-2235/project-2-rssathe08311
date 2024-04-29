# Project Unknown Tomorrow Chase

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

### Student Info

-   Name: Reva Sathe
-   Section: 02

## Simulation Design

This simulation will be based off of a post-apocolyptic zombie-like comic I made during high school.  This project features at the moment 2 agent types. A superpowered human and shades which are husk-like creatures that will go after humans until humans start to attack. This will be a survival simulation, whre players will be able to attempt to aid humans be delivering resources to aid their survival. 

### Controls

-   The user will be able to spawn in more shades to add to the severity of the situation
    -   Left clicking with the mouse on the game screen will spawn in more shades


## Agent 1 - Human

A super powered human adept at fighting off Shades in a post-apocolyptic world. Though if the human cannot fight back they must do their best to run away and survive. There are three humans, all characters from my comic. The first being Leo the main character who is the most adept at fighting off shades but also is the target of the Agressor as he is a defector from a military camp. The second is Tessa, a rouge human fighting to survive all on her own in the post apocolyptic wasteland. The third and final one is Maiki, a young girl who was rescued by Leo after her father turned into a shade.

#### Steering Behaviors

- Obstacle Avoidance - Any sort of barricate or other physical non-moving object will be manuvered around
- Seperation - Humans will seperate from other humans
- Evade - Humans will evade hostile aggressors
- Wander - Roam around the screen if there is no major force acting upon the humans
- Pursue - Humans will hunt down shades, due to their larger group number and proficiencies
- Align - Humans will move in the same direction as other humans
- Cohesion - Humans will move in similar ways as other humans
- Stay In Bounds = Humans will stay within the bounds of the camera
   

## Agent 2 - Shades
Husk like creatures that are a result of the nuclear apocalypse, they are out to hunt any isolated individuals, though against larger groups they turn tail and run. They are creatures of low intelligence and therefore are not able to predict movement well.  

#### Steering Behaviors

- Obstacle Avoidance - Any sort of barricate or other physical non-moving object will be manuvered around
- Seperation - Shades will seperate from other shades
- Flee - Shades will evade humans
- Wander - Roam around the screen if there is no major force acting upon the shades
- Seek - Shades will hunt down aggressors, due to their isolated movement
- Align - Shades will move in the same direction as other shades
- Cohesion - Shades will move in similar ways as other shades
- Stay In Bounds = Shades will stay within the bounds of the camera

## Agent 3 - Aggressor 

A member of the military encampment that Leo escaped determined to get him back and punish him for defecting. Though this organization is more sinester than they first appear. 

#### Steering Behaviors

- Obstacle Avoidance - Any sort of barricate or other physical non-moving object will be manuvered around
- Seperation - Shades will seperate from other shades
- Flee - Shades will evade humans
- Wander - Roam around the screen if there is no major force acting upon the shades
- Seek - Shades will hunt down aggressors, due to their isolated movement
- Align - Shades will move in the same direction as other shades
- Cohesion - Shades will move in similar ways as other shades
- Stay In Bounds = Shades will stay within the bounds of the camera

## Sources
-   The uncompleted webcomic this is based off of: [https://www.webtoons.com/en/canvas/unknown-tomorrow/list?title_no=379290](url)
    - I am the co-author and artist

## Make it Your Own
This project is entirely based off of Unknown Tomorrow, a webcomic I created in highschool. 
- I drew all the art assets for sprites, backgrounds, and obstacles.

## Known Issues

- I am having issues setting up Player Input, I am unable to get the right script and because of this cannot set it up correctly. Though I do have a spawning method written.

### Requirements not completed

_If you did not complete a project requirement, notate that here_

