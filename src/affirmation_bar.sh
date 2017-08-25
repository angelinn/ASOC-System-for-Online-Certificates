echo 'AOSC - Система за автоматично генериране на уверения към СУ' 
echo 'Моля въведете данните си от СУСИ' 

echo
echo 'Потребителско име:'
read username

echo 'Парола:'
read -s password

echo
echo 'Свързване...'

wget --quiet --header="Content-Type: application/json" --post-data="{ \"username\": \"$username\", \"password\":\"$password\" }" https://susiapi.azurewebsites.net/api/affirmation

cat ./affirmation
rm ./affirmation

