function InputManager::create(%this)
{
  echo("input manager created");
}

function InputManager::onTouchDown(%this, %touchID, %worldposition)
{
  echo("worldposition: ", %worldposition);
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
        echo("touched: ", $myobj);
      }
    }
  }
  echo("scene layer: ", $myobj.getSceneLayer());
}

function InputManager::onTouchDragged(%this, %touchID, %worldposition)
{
  if($myobj.getSceneLayer() != 20)
  {
    $myobj.setPosition((mRound(%worldposition.X/5 - 2.5)+2.5)*5, (mRound(%worldposition.Y/5 + 2.5)-2.5)*5);
    echo("pos: ", (mRound(%worldposition.X/5 - 2.5)+2.5)*5, (mRound(%worldposition.Y/5 + 2.5)-2.5)*5);
  }
}

function InputManager::onTouchUp(%this, %touchID, %worldposition)
{
  if ($myobj != null) {
    %count = 0;
    %onBoard = false;
    %picked = mainScene.pickPoint(%worldposition);
    for (%i=0; %i<%picked.count; %i++)
    {
      if (getWord(%picked, %i).getSceneLayer() == 20)
      {
        %onBoard = true;
      }
      else {
        %count += 1;
      }
    }
    if (%count != 1 )
    {
      $overlap.setVisible(true);
      $overlap.schedule(750, "setVisible", false);
      $myobj.setPosition(20, 0);
      $myobj = null;
    }
    else if (!%onBoard) {
      $notOnBoard.setVisible(true);
      $notOnBoard.schedule(750, "setVisible", false);
      $myobj.setPosition(20, 0);
      $myobj = null;
    }
    else
    {
      $myobj = null;
    }
  }
}
