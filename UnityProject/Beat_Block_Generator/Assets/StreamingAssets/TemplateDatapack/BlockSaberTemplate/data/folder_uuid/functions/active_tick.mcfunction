#> Only if song is active does this code run at 20 tps

# Add tick count and spawn notes
execute as @s run function folder_uuid:spawn_notes_base

# Update Fake player eyes and give player items
execute as @s run function _block_saber_base:calculate_fake_eye_height
execute as @s run function _block_saber_base:give_sabers
function folder_uuid:game_controls

# Handle all notes
function _block_saber_base:handle_notes

# Move all nodes
execute as @e[tag=node] run function folder_uuid:move_note

# Clear frame vars
scoreboard players set @s[scores={IsPlayerSneeking=1..}] IsPlayerSneeking 0