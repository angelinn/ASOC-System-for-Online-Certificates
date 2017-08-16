echo 'AOSC - Система за автоматично генериране на уверения към СУ' 
echo 'Моля въведете данните си от СУСИ' 

echo
echo 'Потребителско име:'
read username

echo 'Парола:'
read -s password

echo
echo 'Аутентикиране...'

data=$(curl -s -H "Content-Type: application/json" -X POST -d "{ \"username\": \"$username\", \"password\":\"$password\" }" https://susiapi.azurewebsites.net/api/affirmation)

echo $data
