execute if score #BlockSaberGlobal EndIsLoaded matches 0 in the_end run setblock 0 180 500 minecraft:repeating_command_block{auto:true,Command:"scoreboard players set #BlockSaberGlobal EndIsLoaded 1",TrackOutput:false}
execute if score #BlockSaberGlobal EndIsLoaded matches 0 run execute as @a[gamemode=!spectator] run function _root_class:reset_player_position
execute if score #BlockSaberGlobal EndIsLoaded matches 1 if score #BlockSaberGlobal LevelMapSpawned matches 0 run execute in the_end positioned 0 150.0 500 run function _root_class:setup_level_map
scoreboard players set #BlockSaberGlobal EndIsLoaded 0

function _root_class:give_sabers
execute in minecraft:the_end positioned 0 150.0 500 as @e[type=item,distance=..4,nbt={Item:{tag:{display:{Name:"{\"text\":\"Main Menu\",\"italic\":false}"}}}}] as @p[scores={PlayerPlaying=1}] run execute in the_end positioned 0 150.0 500 run function _root_class:setup_level_map
function _root_class:keep_players_in_bounds