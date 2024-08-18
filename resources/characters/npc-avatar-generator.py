import shutil

for i in range(100):
    shutil.copyfile("NPC000.png", f"./NPC{str(i+1).rjust(3,'0')}.png")
