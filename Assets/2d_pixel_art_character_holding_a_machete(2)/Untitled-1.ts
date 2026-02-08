<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>cave Fighting - rpg edition</title>
    <link rel="stylesheet" href="styles.css">
</head>
<body>
    <h1>Cave Fighting</h1>
    
    <div id="player"></div>
    <div id="experience"></div>
    <br>
    <div id="enemy"></div>
    <hr>

    <button id="attackBtn" onclick="attack()">Attack</button>
    <button id="resetBtn" onclick="resetGame()" style="display:none;">Play Again</button>

    <p id="message"></p>

    <script>
        const player = {
            name: "Hero",
            level: 1,
            exp: 0,
            nextlevel: 100,
            health: 100,
            attackPower: 10,
            critchance: 0.20
        };

        const enemies = [
            {
                name: "goblin",
                health: 80,
                attackPower: 8,
                expvalue: 20
            },
            {
                name: "rock golem",
                health: 160,
                attackPower: 8,
                expvalue: 50
            }
        ];

        let currentEnemyIndex = 0;

        function updateDisplay() {
            document.getElementById("player").innerText = `${player.name} - Level: ${player.level} - Health: ${player.health}`;
            document.getElementById("experience").innerText = `EXP: ${player.exp} / ${player.nextlevel}`;
            document.getElementById("enemy").innerText = `${enemies[currentEnemyIndex].name} - Health: ${enemies[currentEnemyIndex].health}`;
        }

        function checkLevelUp() {
            if (player.exp >= player.nextlevel) {
                player.level++;
                player.exp -= player.nextlevel;
                player.nextlevel = Math.floor(player.nextlevel * 1.5);
                player.health = 100 + (player.level - 1) * 20;
                player.attackPower += 5;
                document.getElementById("message").innerText = `You leveled up to level ${player.level}!`;
            }
        }

        function attack() {
            const crit = Math.random() < player.critchance;
            const damage = crit ? player.attackPower * 2 : player.attackPower;
            enemies[currentEnemyIndex].health -= damage;
            document.getElementById("message").innerText = `You dealt ${damage} damage to the ${enemies[currentEnemyIndex].name}${crit ? ' (Critical Hit!)' : ''}.`;

            if (enemies[currentEnemyIndex].health <= 0) {
                player.exp += enemies[currentEnemyIndex].expvalue;
                document.getElementById("message").innerText += ` You defeated the ${enemies[currentEnemyIndex].name} and gained ${enemies[currentEnemyIndex].expvalue} EXP.`;
                checkLevelUp();
                currentEnemyIndex++;
                if (currentEnemyIndex >= enemies.length) {
                    document.getElementById("message").innerText += " You have defeated all enemies! You win!";
                    document.getElementById("attackBtn").style.display = "none";
                    document.getElementById("resetBtn").style.display = "inline";
                } else {
                    document.getElementById("enemy").innerText = `${enemies[currentEnemyIndex].name} - Health: ${enemies[currentEnemyIndex].health}`;
                    document.getElementById("message").innerText += ` A new enemy appears: ${enemies[currentEnemyIndex].name}!`;
                }
            } else {
                player.health -= enemies[currentEnemyIndex].attackPower;
                document.getElementById("message").innerText += ` The ${enemies[currentEnemyIndex].name} attacks you for ${enemies[currentEnemyIndex].attackPower} damage.`;
                if (player.health <= 0) {
                    document.getElementById("message").innerText += " You have been defeated! Game Over.";
                    document.getElementById("attackBtn").style.display = "none";
                    document.getElementById("resetBtn").style.display = "inline";
                }
                checkLevelUp();
            }

            updateDisplay();
        }

        function resetGame() {
            location.reload();
        }

        updateDisplay();
    </script>
</body>
</html>