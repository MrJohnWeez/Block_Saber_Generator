# Only if song is active does this code run at 20 tps

# Add tick count and spawn notes
function folder_uuid:spawn_notes_base

# Update Fake player eyes and give player items
execute as @p[scores={PlayerPlaying=1}] run function _root_class:calculate_fake_eye_height

# Handle all notes
function _root_class:handle_notes
function _root_class:update_scores

# Move all nodes
execute as @e[tag=node] run function folder_uuid:move_note

# Clear frame vars
scoreboard players set @a[scores={IsPlayerSneeking=1..}] IsPlayerSneeking 0

execute if score #BlockSaberGlobal FinishedObsicles matches 1 if score #BlockSaberGlobal FinishedNotes matches 1 run function folder_uuid:map_difficulty_completed