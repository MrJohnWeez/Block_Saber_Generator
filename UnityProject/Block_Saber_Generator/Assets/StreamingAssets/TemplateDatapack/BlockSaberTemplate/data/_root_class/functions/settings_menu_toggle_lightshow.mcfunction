execute run scoreboard players set #BlockSaberGlobal TempVar1 0
execute if score #BlockSaberGlobal LightShowEnabled matches 0 run scoreboard players set #BlockSaberGlobal TempVar1 1
execute if score #BlockSaberGlobal LightShowEnabled matches 1 run scoreboard players set #BlockSaberGlobal TempVar1 0
execute run scoreboard players operation #BlockSaberGlobal LightShowEnabled = #BlockSaberGlobal TempVar1
execute run scoreboard players set #BlockSaberGlobal TempVar1 0
execute if score #BlockSaberGlobal LightShowEnabled matches 0 run tellraw @a {"text":"Light Show Disabled","color":"yellow"}
execute if score #BlockSaberGlobal LightShowEnabled matches 1 run tellraw @a {"text":"Light Show Enabled","color":"yellow"}
execute as @s run function _root_class:settings_menu
