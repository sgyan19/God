#include <stdlib.h>
#include <stdio.h>

int main(int srgc, char* srgv[]) {
	int i = 0;
	for (i = 0; i < 100; i++) {
		srand(getpid());
		
		printf("rand() = %d\n", rand());
	}
	return 0;
}