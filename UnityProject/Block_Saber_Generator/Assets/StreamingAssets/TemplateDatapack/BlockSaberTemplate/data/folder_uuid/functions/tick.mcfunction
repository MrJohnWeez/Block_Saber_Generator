#> Main Update Loop at 20tps
execute as @e[type=armor_stand,tag=showTitle,tag=blockBeat,tag=!SONGID] run function folder_uuid:display_title
execute if score #BlockSaberGlobal SongID matches SONGID if score #BlockSaberGlobal PlayingSong matches 1 run function folder_uuid:active_tick
execute if score #BlockSaberGlobal SongID matches SONGID run function folder_uuid:game_controls

# Temp debug info
# scoreboard players operation MrJohnWeez CopyTemp = #BlockSaberGlobal TickCount
