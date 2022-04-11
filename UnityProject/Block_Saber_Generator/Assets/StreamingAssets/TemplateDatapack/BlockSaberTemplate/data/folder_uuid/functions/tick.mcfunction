# Main Update Loop at 20tps
execute if score #BlockSaberGlobal SongID matches SONGID if score #BlockSaberGlobal PlayingSong matches 1 run function folder_uuid:active_tick
execute if score #BlockSaberGlobal SongID matches SONGID run function folder_uuid:game_controls

scoreboard players set #BlockSaberGlobal TempVar4 0
execute store result score #BlockSaberGlobal TempVar4 run scoreboard players operation #BlockSaberGlobal SONGDIFFICULTYID = #BlockSaberGlobal SONGDIFFICULTYID
execute if score #BlockSaberGlobal TempVar4 matches 0 run function folder_uuid:init
execute if score #BlockSaberGlobal SONGDIFFICULTYID = #BlockSaberGlobal SONGDIFFICULTYID as @e[type=armor_stand,tag=showTitle,tag=blocksaber,tag=!SONGID] run function folder_uuid:display_title
scoreboard players set #BlockSaberGlobal TempVar4 0