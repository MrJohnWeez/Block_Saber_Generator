#> Main Update Loop at 20tps

execute as @e[type=armor_stand,tag=showTitle,tag=blockBeat,tag=!SONGID] run function folder_uuid:display_title


# Add tick count and spawn notes
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongID=SONGID}] run function folder_uuid:spawn_notes_base

# Update Fake player eyes and give player items
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongID=SONGID}] run function _block_saber_base:calculate_fake_eye_height
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongID=SONGID}] run function _block_saber_base:give_sabers
function folder_uuid:game_controls

# Handle all notes
function _block_saber_base:handle_notes

# Move all nodes
execute as @e[tag=node] run function folder_uuid:move_note

# Clear frame vars
execute at @e[type=armor_stand,tag=playerOrgin] run scoreboard players set @p[scores={SongID=SONGID,IsPlayerSneeking=1..}] IsPlayerSneeking 0