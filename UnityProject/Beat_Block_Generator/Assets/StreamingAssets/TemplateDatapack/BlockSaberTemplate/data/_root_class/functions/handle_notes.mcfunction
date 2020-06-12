# Update all notes health
execute as @e[tag=note] store result score @s NoteHealth run data get entity @s Health

# Calculate tick score
execute as @e[tag=noteBlue] run function _root_class:check_right_hit_notes
execute as @e[tag=noteRed] run function _root_class:check_left_hit_notes
execute as @e[tag=noteBomb] run function _root_class:check_bomb_collide
execute as @e[tag=box] run function _root_class:check_red_wall
execute as @e[type=armor_stand,tag=playerOrgin] run function _root_class:check_for_missed_items