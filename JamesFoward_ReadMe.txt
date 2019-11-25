READ ME

First let me start with my plan: 

I wanted to create a game similiar to "Lightbot: Code Hour"
https://play.google.com/store/apps/details?id=com.lightbot.lightbothoc

This is an educational game designed to introduce children to programming. 

Full Disclosure: The A* section of this test I found online and heavily modifed to fit this problem. 


1. I would create a graph of nodes, Populate it with a Start Point, And End Point and Obstacles. 
2. I would use A* pathfinding to ensure the Start and End Point always had a valid connection. 
3. I would restart the game If 
  A - There was No Path
  B - The Path Was Too Short 
4. I used interfaces for inputing commands. 
5. When adding a command it is added to a Queue of commands.
6. To Play, the game just steps through each command in the queue. 

Scalability
1. I used Scriptable Objects for creating new Levels.


NOTE: Only play in editor, please read Debug Logs/Errors for win and lose states. 
NOTE: The game randomly cycles between the two levels. 
How To Play: 
1. Input Commands. 
2. Press Go 


What I would of done had I spent more time on it. 
1. Added more commands. 
2. Uses a Random Seed to save/load popular levels. 
3. Added unique blocks to each level Scriptable Object. 