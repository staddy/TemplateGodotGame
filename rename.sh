#!/bin/bash

old_name="TemplateGodotGame"
input_string="${1// /}"

sed -i "s/${old_name}/${input_string}/g" ./${old_name}.sln
sed -i s/assembly_name=\"${old_name}/assembly_name=\"${input_string}/g ./project.godot
sed -i "s/${old_name}/$1/g" ./project.godot

mv ./${old_name}.sln ./${input_string}.sln
mv ./${old_name}.csproj ./${input_string}.csproj

#game_dir="$(basename "$(pwd)")"
#cd ..
#mv ./${game_dir} ./${input_string}

echo "Renaming done!"
