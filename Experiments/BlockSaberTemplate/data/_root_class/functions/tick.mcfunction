execute if score #BlockSaberGlobal EndIsLoaded matches 0 in the_end run setblock 0 240 500 minecraft:repeating_command_block{auto:true,Command:"scoreboard players set #BlockSaberGlobal EndIsLoaded 1",TrackOutput:false}
execute if score #BlockSaberGlobal EndIsLoaded matches 0 run execute as @a run function _root_class:reset_player_position
execute if score #BlockSaberGlobal EndIsLoaded matches 1 if score #BlockSaberGlobal LevelMapSpawned matches 0 run execute in the_end positioned 0 150 500 run function _root_class:setup_level_map
scoreboard players set #BlockSaberGlobal EndIsLoaded 0

function _root_class:take_care_of_players