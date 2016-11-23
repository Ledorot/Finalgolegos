#include <iostream>
#include <cstdlib>
#include <ctime>

int main (int argc, char ** argv) {

	bool Done = false;

	// Do this is many times as user wishes
	while (!Done) {
		system ("cls");

		int PlayerAtkCount;
		int EnemyAtkCount;
		int PlayerDefCount;
		int EnemyDefCount;
		int PlayerHealth;
		int EnemyHealth;
		int Iterations;

		// Grab simulation parameters
		std::cout << "Enter PLAYER attack dice count: ";
		std::cin >> PlayerAtkCount;
		std::cout << "Enter PLAYER defense dice count: ";
		std::cin >> PlayerDefCount;
		std::cout << "Enter ENEMY attack dice count: ";
		std::cin >> EnemyAtkCount;
		std::cout << "Enter ENEMY defense dice count: ";
		std::cin >> EnemyDefCount;
		std::cout << "Enter PLAYER health: ";
		std::cin >> PlayerHealth;
		std::cout << "Enter ENEMY heatlh: ";
		std::cin >> EnemyHealth;
		std::cout << "Enter the matchup test duration (1 - 100000): ";
		std::cin >> Iterations;

		srand (time (NULL));

		int PlayerWinCount = 0;
		int EnemyWinCount = 0;
		int AverageTurns = 0;
		int AverageDiff = 0;

		for (int i = 0; i < Iterations; i++) {

			//std::cout << "Iteration: " << i << std::endl;

			int playerHealth = PlayerHealth;
			int enemyHealth = EnemyHealth;
			int averageDiff = 0;

			int d = 0;
			int diff = 0;

			int turns = 0;

			while (!(playerHealth <= 0 || enemyHealth <= 0)) {

				int PlayerAtkTotal = 0;
				int PlayerDefTotal = 0;
				int EnemyAtkTotal = 0;
				int EnemyDefTotal = 0;

				//std::cout << "\tPLAYER is attacking..." << std::endl;

				// Player attacks first
				for (d = 0; d < PlayerAtkCount; d++) {
					PlayerAtkTotal += rand () % 6 + 1;
				}

				for (d = 0; d < EnemyDefCount; d++) {
					EnemyDefTotal += rand () % 6 + 1;
				}

				//std::cout << "\tPLAYER attacked with " << PlayerAtkTotal << " and ENEMY defended with " << EnemyDefTotal << "." << std::endl;

				diff = PlayerAtkTotal - EnemyDefTotal;

				averageDiff += diff;

				if (diff > 0) {
					enemyHealth -= diff;
					//std::cout << "\tENEMY health: " << enemyHealth << std::endl;
				}

				//std::cout << "\tPLAYER is done attacking." << std::endl;

				//std::cout << "\tENEMY is attacking..." << std::endl;

				// Enemy attacks second
				for (d = 0; d < EnemyAtkCount; d++) {
					EnemyAtkTotal += rand () % 6 + 1;
				}

				for (d = 0; d < PlayerDefCount; d++) {
					PlayerDefTotal += rand () % 6 + 1;
				}

				//std::cout << "\tENEMY attacked with " << EnemyAtkCount << " and PLAYER defended with " << PlayerDefCount << "." << std::endl;

				diff = EnemyAtkTotal - PlayerDefTotal;

				averageDiff += diff;

				if (diff > 0) {
					playerHealth -= diff;
					//std::cout << "\tPLAYER health: " << playerHealth << std::endl;
				}

				//std::cout << "\tENEMY is done attacking." << std::endl;

				turns++;
			}

			if (playerHealth > 0) {
				PlayerWinCount++;
			}

			if (enemyHealth > 0) {
				EnemyWinCount++;
			}

			//std::cout << "PLAYER Win Count: " << PlayerWinCount << std::endl;
			//std::cout << "ENEMY Win Count: " << EnemyWinCount << std::endl;

			AverageTurns += turns;
			AverageDiff = averageDiff / (turns * 2);
		}

		AverageTurns /= Iterations;
		double PlayerWinPercentage = (double)PlayerWinCount / (double)Iterations;

		std::cout << std::endl << std::endl <<
			"After " << Iterations << " iterations:" << std::endl <<
			"The PLAYER win rate was " << PlayerWinPercentage << "." << std::endl <<
			"The battle tended to last " << AverageTurns << " turns." << std::endl <<
			"The average difference in rolls was " << AverageDiff << "." << std::endl;

		std::cout << "Enter 0 to start again, or 1 to quit: ";
		std::cin >> Done;
	}

	return 0;
}