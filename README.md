# FLAI - A Flying AI

FLAI is an Unity ml-agents based AI. It's an airplane jet that learns how to collide with packages in the sky.

## Getting Started

These are the instructions on setting up ml-agents to train the agent.

### Installing en Setting Up.

A step by step guide on how to open and use the Unity Project.

#### Installing
First you need to the download the whole repository and save it to a folder.

```
Click on the Green button that says: "↓ Code", then click on download ZIP
```

#### Setting Up
After the download is completed. 

```
Unpack the zip and move the folder to a more accasible folder.
(I would suggest a place close or in your mother directory: "C:" so it's easy to acces from your command prompt)
```

Setting up in Unity

```
Open 'Unity Hub' and click 'Add' at the upper right part of the window.
Now move to inside the FLAI-main folder and click on the Unity Project folder and select this folder.
```

Now the Unity Project is installed and set-up, can we move on to Training and Playing the game.

## Running and Training

Explaining how to play and train the Agents. So open up the Unity Project from Unity Hub.

### Playing the Agents

#### Setting up

Turn all parameters to playing.
``` 
Inside the Unity Editor, Click on Flight Area in the scene Hierarchy.
(If there are more than 1, probably 8 remove them so there's only 1.)

Then click on the left of the name in the hierarchy so you see all the gameObjects inside.

Click on the Jet Object inside Flight Area.

In the inspector, go to Behavior Parameters and set Behavior type to 'Hueristic Only'

You're Done!
```

#### Playing

Play the Agent
```
Just click on play in the upper middle of the screen, it's an triangle.
```

### Training the Agents

Training the agents to let them learn how to fly around.

#### First time setup: 
Create an env for ml-agents in Anaconda
```
conda create -n [name] python=3.7
``` 

Activate the env
```
conda activate [name]
```

Install mlagents
```
pip install mlagents
```

#### Training the Agents
First we have to set some parameters in the Unity Editor.
```
Go to the scene Hierarchy and go the 'Jet' under the 'Flight Area' gameObject.
In the inspector go to Behavior Parameters and set Behavior Type to 'default' if you changed it in the past.
If you made any changes, click on 'FLight Area' and in the inspector in the upper right click on overrides and press apply changes.
If you only have 1 flight area copy the flight area and duplicate it till you have 8, I suggest them 5000 units appart.
```

Open the Anaconda Prompt, and activate the env
```
conda activate [name]
```

Move to the FLAI-main folder you've downloaded.
```
using cd to go in and .. to go out of a folder
```

Move in to the "Config and results" folder.
```
cd Config and results
```

Start the learning
```
mlagents-learn ./Improved_config.yaml --run-id [Name for the learning run]
```

Start the game
```
In the Unity Editor press the triangle in the upper middle to start playing and training.
```

#### Seeing your progress

Open the data with tensorboard
```
In the "Config and results" folder:
tensorboard --logdir results
```

## Built With

* [Mlagents](https://github.com/Unity-Technologies/ml-agents) - The AI used
* [Unity](https://unity.com/) - The Game Engine Used

## Authors

* **Abbe Engers**

## Acknowledgments

* Credits to Adam Kelly for the tutorials on mlagents
