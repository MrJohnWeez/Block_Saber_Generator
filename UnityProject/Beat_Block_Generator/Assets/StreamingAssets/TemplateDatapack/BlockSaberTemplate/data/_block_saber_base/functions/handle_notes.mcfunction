# Update all notes health
execute as @e[tag=note] store result score @s NoteHealth run data get entity @s Health

# Calculate tick score
execute as @e[tag=noteBlue] run function _block_saber_base:check_right_hit_notes
execute as @e[tag=noteRed] run function _block_saber_base:check_left_hit_notes
execute as @e[tag=noteBomb] run function _block_saber_base:check_bomb_collide
execute as @e[tag=box] run function _block_saber_base:check_red_wall
execute as @e[type=armor_stand,tag=playerOrgin] run function _block_saber_base:check_for_missed_items