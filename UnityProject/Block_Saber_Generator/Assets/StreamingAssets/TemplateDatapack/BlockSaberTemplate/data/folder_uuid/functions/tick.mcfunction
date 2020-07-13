#> Main Update Loop at 20tps
execute if score #BlockSaberGlobal SongID matches SONGID if score #BlockSaberGlobal PlayingSong matches 1 run function folder_uuid:active_tick
execute if score #BlockSaberGlobal SongID matches SONGID run function folder_uuid:game_controls
