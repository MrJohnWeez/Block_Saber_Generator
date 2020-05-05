#> Main Update Loop at 20tps

#Update Fake player eyes
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongUUID=1111}] run function block_saber_base:calculate_fake_eye_height

execute as @e[tag=note] store result score @s NoteHealth run data get entity @s Health

scoreboard players operation #TEST NoteHealth = @e[tag=note,limit=1] NoteHealth




execute as @e[tag=noteBlue] run function block_saber_base:check_right_hit_notes
execute as @e[tag=noteRed] run function block_saber_base:check_left_hit_notes
execute as @e[tag=noteBomb] run function block_saber_base:check_bomb_collide
execute as @e[tag=box] run function block_saber_base:check_red_wall

execute as @e[tag=node] run function block_saber_base:move_note

#Clear frame vars
execute at @e[type=armor_stand,tag=playerOrgin] run scoreboard players set @p[scores={SongUUID=1111,IsPlayerSneeking=1..}] IsPlayerSneeking 0
execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongUUID=1111}] run function block_saber_base:check_for_falling
