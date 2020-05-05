execute if score @s NoteHealth > #CONST10 Var1 run say +1 Correct Right
execute if score @s NoteHealth > #CONST10 Var1 at @s run kill @e[type=armor_stand,distance=..0.3]
execute if score @s NoteHealth > #CONST10 Var1 run kill @s
execute if score @s NoteHealth < #CONST10 Var1 run say -1 Wrong Right
execute if score @s NoteHealth < #CONST10 Var1 at @s run kill @e[type=armor_stand,distance=..0.3]
execute if score @s NoteHealth < #CONST10 Var1 run kill @s