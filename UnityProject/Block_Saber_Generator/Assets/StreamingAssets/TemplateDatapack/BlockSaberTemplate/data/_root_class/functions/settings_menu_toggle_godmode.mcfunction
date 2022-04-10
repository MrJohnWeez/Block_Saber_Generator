execute run scoreboard players set #BlockSaberGlobal TempVar1 0
execute if score #BlockSaberGlobal GodModeEnabled matches 0 run scoreboard players set #BlockSaberGlobal TempVar1 1
execute if score #BlockSaberGlobal GodModeEnabled matches 1 run scoreboard players set #BlockSaberGlobal TempVar1 0
execute run scoreboard players operation #BlockSaberGlobal GodModeEnabled = #BlockSaberGlobal TempVar1
execute run scoreboard players set #BlockSaberGlobal TempVar1 0
execute if score #BlockSaberGlobal GodModeEnabled matches 0 run tellraw @a {"text":"God Mode Disabled","color":"yellow"}
execute if score #BlockSaberGlobal GodModeEnabled matches 1 run tellraw @a {"text":"God Mode Enabled","color":"yellow"}
execute as @s run function _root_class:settings_menu