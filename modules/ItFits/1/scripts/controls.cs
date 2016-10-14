function InputManager::create(%this)
{
  echo("input manager created");
}

function InputManager::onTouchDown(%this, %touchID, %worldposition)
{
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    $myobj = getWord(%picked, %i);
    echo("touched: ", $myobj);
  }
}

function InputManager::onTouchDragged(%this, %touchID, %worldposition)
{
  echo("worldposition: ", mRound(%worldposition.X));
  $myobj.setPosition(mRound(%worldposition.X), mRound(%worldposition.Y));
}

function InputManager::onTouchUp(%this, %touchID, %worldposition)
{
  $myobj = null;
}
