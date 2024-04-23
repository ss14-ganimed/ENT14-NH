# Commands


## Delay shuttle round end

emergency-shuttle-command-round-desc = Останавливает таймер окончания раунда, когда капсулы покидает гиперпространство.
emergency-shuttle-command-round-yes = Раунд продлён.
emergency-shuttle-command-round-no = Невозможно продлить окончание раунда.

## Dock emergency shuttle

emergency-shuttle-command-dock-desc = Вызывает спасательную капсулу и пристыковывает его к станции... если это возможно.

## Launch emergency shuttle

emergency-shuttle-command-launch-desc = Досрочно запускает капсулы, если это возможно.
# Emergency shuttle
emergency-shuttle-left = Спасательные капсулы покинули корабль. Расчетное время полёта - { $transitTime } секунд.
emergency-shuttle-launch-time = Спасательные капсулы покинут корабль через { $consoleAccumulator } секунд.
emergency-shuttle-docked = Спасательные капсулы готовы к отлёту. Они улетят через { $time } секунд.
emergency-shuttle-good-luck = Эвакуационный шаттл не может найти станцию. Удачи.
emergency-shuttle-nearby = Спасательные капсулы готовы к отлёту. Они улетят через { $time } секунд.
# Emergency shuttle console popup / announcement
emergency-shuttle-console-no-early-launches = Досрочный запуск отключён
# Emergency shuttle console popup / announcement
emergency-shuttle-console-auth-left =
    { $remaining } { $remaining ->
        [one] авторизация осталась
        [few] авторизации остались
       *[other] авторизации остались
    } для досрочного запуска капмулы.
emergency-shuttle-console-auth-revoked =
    Авторизации на досрочный запуск капсулы отозваны, { $remaining } { $remaining ->
        [one] авторизация необходима
        [few] авторизации необходимы
       *[other] авторизации необходимы
    }.
emergency-shuttle-console-denied = Доступ запрещён
# UI
emergency-shuttle-console-window-title = Консоль спасательной капсулы
# UI
emergency-shuttle-ui-engines = ДВИГАТЕЛИ:
emergency-shuttle-ui-idle = Простой
emergency-shuttle-ui-repeal-all = Повторить всё
emergency-shuttle-ui-early-authorize = Разрешение на досрочный запуск
emergency-shuttle-ui-authorize = АВТОРИЗОВАТЬСЯ
emergency-shuttle-ui-repeal = ПОВТОРИТЬ
emergency-shuttle-ui-authorizations = Авторизации
emergency-shuttle-ui-remaining = Осталось: { $remaining }
