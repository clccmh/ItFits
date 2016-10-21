function InputManager::create(%this)
{
  echo("input manager created");
}

function InputManager::onTouchDown(%this, %touchID, %worldposition)
{
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    if ($myobj == null)
    {
      if (getWord(%picked, %i).getSceneLayer() != 20)
      {
        $myobj = getWord(%picked, %i);
      }
    }
    else if ($myobj.getSceneLayer() > getWord(%picked, %i).getSceneLayer())
    {
      if (getWord(%picked, %i).getSceneLayer() != 20)
      {
        $myobj = getWord(%picked, %i);
      }
    }
  }
}

function InputManager::onTouchDragged(%this, %touchID, %worldposition)
{
  if($myobj.getSceneLayer() != 20)
  {
    //echo("Position: ", (mRound(%worldposition.X/5 - 2.5)+2.5)*5, " ", (mRound(%worldposition.Y/5 + 2.5)-2.5)*5);
    $myobj.setPosition((mRound(%worldposition.X/5 - 2.5)+2.5)*5, (mRound(%worldposition.Y/5 + 2.5)-2.5)*5);
    //$myobj.setPosition(%worldposition);
  }
}

function InputManager::onTouchUp(%this, %touchID, %worldposition)
{
  if ($myobj != null)
  {
    if (!%this.isOnBoard(%worldposition))
    {
      $notOnBoard.setVisible(true);
      $notOnBoard.schedule(750, "setVisible", false);
      $myobj.setPosition(20, 0);
    }
    else if (%this.testOverlap(%worldposition))
    {
      $overlap.setVisible(true);
      $overlap.schedule(750, "setVisible", false);
      $myobj.setPosition(20, 0);
    }
    $myobj = null;
  }
}

function InputManager::theSameBlock(%this, %sprite1, %sprite2)
{
  for (%i=1; %i <= %sprite1.getSpriteCount(); %i++)
  {
    %sprite1.selectSpriteId(%i);
    %sprite2.selectSpriteId(%i);
    if (%sprite1.getSpriteLocalPosition() != %sprite2.getSpriteLocalPosition())
    {
      return false;
    }
  }
  return true;
}

function InputManager::testOverlap(%this, %worldposition) {
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    %sprite = getWord(%picked, %i);
    if (%sprite.getSceneLayer() != 20)
    {
      for (%x = 1; %x <= %sprite.getSpriteCount(); %x++)
      {
        %sprite.selectSpriteId(%x);
        %blockPosition = (%sprite.getPosition().X + %sprite.getSpriteLocalPosition().X SPC %sprite.getPosition().Y + %sprite.getSpriteLocalPosition().Y);
        %secondPicked = mainScene.pickPoint(%blockPosition);
        for (%k = 0; %k < %secondPicked.count; %k++)
        {
          %secondSprite = getWord(%secondPicked, %k);
          if (%secondSprite.getSceneLayer() != 20)
          {
            for (%y = 1; %y <= %secondSprite.getSpriteCount(); %y++)
            {
              if (!%this.theSameBlock(%sprite, %secondSprite))
              {
                %secondSprite.selectSpriteId(%y);
                %secondBlockPosition = (%secondSprite.getPosition().X + %secondSprite.getSpriteLocalPosition().X SPC %secondSprite.getPosition().Y + %secondSprite.getSpriteLocalPosition().Y);
                echo("Block ", %i, " position: ", %blockPosition, ", Block ", %k, " position: ", %secondBlockPosition);
                if (%secondBlockPosition.X == %blockPosition.X && %secondBlockPosition.Y == %blockPosition.Y)
                {
                  echo("FAILED: Block ", %i, " position: ", %blockPosition, ", Block ", %k, " position: ", %secondBlockPosition);
                  return true;
                }
              }
            }
          }
        }
      }
    }
  }
  return false;
}


function InputManager::hasBlockOverlap(%this, %worldposition) {
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    %sprite = getWord(%picked, %i);
    if (%sprite.getSceneLayer() != 20)
    {
      for (%x = 1; %x <= %sprite.getSpriteCount(); %x++)
      {
        %sprite.selectSpriteId(%x);
        %blockPosition = (%sprite.getPosition().X + %sprite.getSpriteLocalPosition().X SPC %sprite.getPosition().Y + %sprite.getSpriteLocalPosition().Y);
        if (%this.anySquaresOverlap(%blockPosition)) {
          return true;
        }
      }
    }
  }
  return false;
}

function InputManager::anySquaresOverlap(%this, %worldposition) {
  %count = 0;
  %total = 0;
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    %sprite = getWord(%picked, %i);
    if (%sprite.getSceneLayer() != 20)
    {
      for (%x = 1; %x <= %sprite.getSpriteCount(); %x++)
      {
        %sprite.selectSpriteId(%x);
        echo("Sprite: ", %i, " X: ", %sprite.getSpriteLocalPosition().X, " Y: ", %sprite.getSpriteLocalPosition().Y);
        %blockPosition = (%sprite.getPosition().X + %sprite.getSpriteLocalPosition().X SPC %sprite.getPosition().Y + %sprite.getSpriteLocalPosition().Y);
        if (mainScene.pickPoint(%blockPosition).count-1 > %total)
        {
          %total = mainScene.pickPoint(%blockPosition).count-1;
        }
        if (%this.isOverlapping(%blockPosition))
        {
          %count++;
        }
      }
    }
  }
  echo("Count: ", %count, " Total: ", %total);
  if (%total > 1 && %count == %total) {
    return true;
  }
  return false;
}

function InputManager::isOverlapping(%this, %worldposition)
{
  %count = 0;
  %picked = mainScene.pickPoint(%worldposition);
  if (%picked.count > 2)
  {
    echo("overlap at: ", %worldposition);
    return true;
  }
  return false;
}

function InputManager::isOnBoard(%this, %worldposition)
{
  %count = 0;
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    %sprite = getWord(%picked, %i);
    if (%sprite.getSceneLayer() != 20)
    {
      for (%x = 1; %x <= %sprite.getSpriteCount(); %x++)
      {
        %sprite.selectSpriteId(%x);
        echo("Sprite: ", %i, " X: ", %sprite.getSpriteLocalPosition().X, " Y: ", %sprite.getSpriteLocalPosition().Y);
        %blockPosition = (%sprite.getPosition().X + %sprite.getSpriteLocalPosition().X SPC %sprite.getPosition().Y + %sprite.getSpriteLocalPosition().Y);
        %blockPoint = mainScene.pickPoint(%blockPosition);
        for (%y = 0; %y < %blockPoint.count; %y++)
        {
          if (getWord(%blockPoint, %y).getSceneLayer() == 20)
          {
            %count++;
          }
        }
      }
    }
  }

  if (%picked.count > 1 && %count == (%picked.count-1)*4)
  {
    return true;
  }
  return false;
}
