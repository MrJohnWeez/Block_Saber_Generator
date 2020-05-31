#> Main Update Loop at 20tps
execute as @e[type=armor_stand,tag=showTitle,tag=blockBeat,tag=!SONGID] run function folder_uuid:display_title
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongID=SONGID,PlayingSong=1}] run function folder_uuid:active_tick
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongID=SONGID}] run function folder_uuid:game_controls
