# Projekt: HitmAIn

Projekt ten polega na stworzeniu prostej gry w Unity oraz testowaniu na niej agentów sztucznej inteligencji, którzy przechodzą tę grę dzięki wykorzystaniu metod uczenia ze wzmocnieniem (Reinforcement Learning).

## Spis treści

- [Opis](#opis)
- [Funkcje](#funkcje)
- [Instalacja](#instalacja)
- [Wyniki](#wyniki)
- [Licencja](#licencja)

## Opis

Celem projektu jest stworzenie gry w Unity, w której graczem może być zarówno człowiek, jak i agent sztucznej inteligencji. Agent uczy się rozwiązywać poziomy gry poprzez interakcję z otoczeniem, optymalizując swoje decyzje dzięki algorytmom uczenia ze wzmocnieniem. Projekt został wykonany w ramach zajęć na uczelni, a jego głównym założeniem jest zbadanie efektywności różnych podejść AI w kontekście gier komputerowych.

## Funkcje

- Prosta gra stworzona w silniku Unity
- Integracja agenta AI zdolnego do przechodzenia poziomów
- Możliwość definiowania zasad gry i poziomów trudności
- Wizualizacja wyników i analizy zachowania agenta
- Eksperymenty z różnymi algorytmami uczenia AI

## Instalacja

Kroki, które trzeba wykonać, aby uruchomić projekt:

1. Upewnij się, że masz zainstalowany Unity Hub i odpowiednią wersję Unity (informacja o wersji w pliku `ProjectSettings/ProjectVersion.txt`).
2. Sklonuj repozytorium:

```bash
$ git clone https://github.com/uzytkownik/projekt-unity.git
```

3. Otwórz projekt w Unity Hub.
4. (Opcjonalnie) Jeśli chcesz używać agenta AI, zainstaluj Condę, a następnie środowisko:

```bash
$ conda create --name ai_env python=3.9
$ conda activate ai_env
$ pip install -r requirements.txt
```

## Wyniki

W ramach projektu przeprowadzono szereg eksperymentów mających na celu zbadanie efektywności różnych algorytmów uczenia ze wzmocnieniem w kontekście gry. Wyniki przedstawiono na poniższych wykresach:

### Porównanie algorytmów

Wykres przedstawiający porównanie średniej liczby kroków potrzebnych agentowi do ukończenia poziomu:

![Porównanie algorytmów](results/algorithms_comparison.png)

### Uczenie agenta

Krzywa uczenia agenta dla wybranego algorytmu (średni reward w czasie):

![Krzywa uczenia](results/learning_curve.png)

### Efektywność w różnych scenariuszach

Wyniki eksperymentów dla różnych poziomów trudności gry:

![Efektywność](results/scenarios_performance.png)

## Licencja

Ten projekt jest objęty licencją MIT - więcej informacji znajdziesz w pliku [LICENSE](LICENSE).


Ten projekt jest objęty licencją MIT - więcej informacji znajdziesz w pliku LICENSE.
