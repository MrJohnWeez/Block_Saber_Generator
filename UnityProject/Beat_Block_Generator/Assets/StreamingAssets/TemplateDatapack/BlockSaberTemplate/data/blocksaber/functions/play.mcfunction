tellraw @s {"text":"Welcome to Block Saber:","color":"yellow"}
#https://minecraft.tools/en/tellraw.php?tellraw=%3Cspan%20style%3D%22color%3A%23FFFF55%22%3EWelcome%20to%20Block%20Saber%3A%3C%2Fspan%3E&selector=%40s

kill @e[type=armor_stand,tag=showTitle,tag=blockBeat]
execute at @s as @s run summon armor_stand ~ ~ ~ {Tags:[showTitle,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}