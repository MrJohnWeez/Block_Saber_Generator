execute if score @s NoteHealth < #CONST3 Const_3 at @s as @e[type=rabbit,tag=note,distance=..6] run function block_saber_base:correct_auto_hit_row_notes
execute if score @s NoteHealth < #CONST3 Const_3 run function block_saber_base:note_was_correct
execute if entity @s[name="Blue Saber"] at @s as @e[type=rabbit,tag=note,distance=..6] run function block_saber_base:wrong_auto_hit_row_notes
execute if entity @s[name="Blue Saber"] run function block_saber_base:note_was_wrong