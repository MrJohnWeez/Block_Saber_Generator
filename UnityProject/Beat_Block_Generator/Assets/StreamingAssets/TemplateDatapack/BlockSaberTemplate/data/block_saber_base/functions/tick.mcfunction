#> Main Update Loop at 20tps

execute as @e[type=armor_stand,tag=showTitle,tag=blockBeat,tag=!SONGUUID] run function block_saber_base:display_title


# Add tick count and spawn notes
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongUUID=SONGUUID}] run function block_saber_base:spawn_notes_base

# Update Fake player eyes and give player items
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongUUID=SONGUUID}] run function block_saber_base:calculate_fake_eye_height
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongUUID=SONGUUID}] run function block_saber_base:give_sabers

# Update all notes health
execute as @e[tag=note] store result score @s NoteHealth run data get entity @s Health

# Calculate tick score
execute as @e[tag=noteBlue] run function block_saber_base:check_right_hit_notes
execute as @e[tag=noteRed] run function block_saber_base:check_left_hit_notes
execute as @e[tag=noteBomb] run function block_saber_base:check_bomb_collide
execute as @e[tag=box] run function block_saber_base:check_red_wall
execute as @e[type=armor_stand,tag=playerOrgin] run function block_saber_base:check_for_missed_items

# Move all nodes
execute as @e[tag=node] run function block_saber_base:move_note

# Clear frame vars
execute at @e[type=armor_stand,tag=playerOrgin] run scoreboard players set @p[scores={SongUUID=SONGUUID,IsPlayerSneeking=1..}] IsPlayerSneeking 0
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongUUID=SONGUUID}] run function block_saber_base:check_for_falling



