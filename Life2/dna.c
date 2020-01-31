#include <stdlib.h>
#include <stdio.h>

int main(int srgc, char* srgv[]) {
	int i = 0, j = 0, k = 0,r = 0;
	char sn[1024];
	FILE* t, * s;
	if (srgc > 1) {
		i = atoi(srgv[1]);
	}
	else {
		i = getpid();
	}
	srand(i);
	i = 0;
	while (srgv[0][i] != '\0') {
		sn[i] = srgv[0][i];
		i++;
	}
	sn[i] = '\0';
	t = fopen(sn, "rb");
	if (!t) {
		return -1;
	}
	if (i > 500) {
		i = k + 5;
	}
	j = rand();
	j = abs(j) % 36 + '0';
	j = j <= '9' ? j : j + 7;
	sn[i] = j;
	sn[i + 1] = '\0';
	s = fopen(sn, "wb");
	if (!s) {
		return -1;
	}
	while ((i = fread(sn, 1, 1, t)) != 0) {
		if (!(rand() % 10000)) {
			k = rand();
			if (k & 0x1) {
				k = rand();
				fwrite(&k, 1, 1, s);
				fwrite(sn, 1, 1, s);
			}
			else if (k & 0b10) {
				fwrite(&j, 1, 1, s);
			}
		}
		else {
			fwrite(sn, 1, 1, s);
		}
	}
	fclose(t);
	fclose(s);
	return 0;
}