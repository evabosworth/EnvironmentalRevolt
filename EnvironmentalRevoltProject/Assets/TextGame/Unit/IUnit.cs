using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUnit : IWorldObject
{

	//squares you can move in cardinal directions
	protected List<IAttack> attacks;
	protected IPlayer unitOwner;
    protected IMove move;
    protected int experienceTotal = 0;
    protected int curLevel = 1;

    public int strength { get; set; }
    public int physicalDefense { get; set; }
    public int intelligence { get; set; }
    public int magicDefense { get; set; }
    public int dexterity { get; set; }


    // to be changed when more information is stored for players
    protected int playerNumber = int.MinValue;
    public int getPlayerNumber()
    {
        return playerNumber;
    }
    public void setPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }



    public int getJumpHeight()
    {
        return this.move.getJumpHeight();
    }

    public IMove getMovementType()
    {
        return move;
    }

    public int getMovement()
    {
        return move.getMovement();
    }

    public IUnit(int movement, int jumpHeight)
    {
        move = new BasicMove(movement, jumpHeight);
    }

	public List<IAttack> Attacks {
		get {
			return attacks;
		}
	}

    public int getCurLevel()
    {
        return curLevel;
    }

    public int getExperienceTotal()
    {
        return experienceTotal;
    }




    public void addExperienceToUnit(int amountToAdd)
    {
        experienceTotal += amountToAdd;
        checkIfLevelUp();
    }

    private bool checkIfLevelUp()
    {
		int levelCount = ExperienceCurve.getLevelFromStandardExp (experienceTotal);

        if(curLevel < levelCount)
        {

            //increases each stat by 1 with each level, TODO fix to be more interesting
            strength += levelCount - curLevel;
            physicalDefense += levelCount - curLevel;
            intelligence += levelCount - curLevel;
            magicDefense += levelCount - curLevel;
            dexterity += levelCount - curLevel;
            curLevel = levelCount;
            return true;
        }

        return false;

    }


}

