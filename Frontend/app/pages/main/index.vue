<template>
  <n-config-provider :theme="darkTheme">
    <n-layout style="min-height: 100vh; background-color: #18181c">
      <n-layout-header bordered style="padding: 16px 24px">
        <n-space justify="space-between" align="center">
          <n-h2 style="margin: 0">🏗️ Revit Plugin Hub</n-h2>
          <n-text depth="3">v1.0.0</n-text>
        </n-space>
      </n-layout-header>

      <n-layout-content style="padding: 24px">
        <n-grid x-gap="24" y-gap="24" cols="1 s:2 m:3 l:4" responsive="screen">
          <n-grid-item v-for="plugin in pluginList" :key="plugin.id">
            <n-card
              :title="plugin.name"
              hoverable
              size="medium"
              style="height: 100%; border-radius: 8px"
            >
              <template #header-extra>
                <n-badge :type="plugin.isStable ? 'success' : 'warning'" dot />
              </template>

              <n-text depth="2">{{ plugin.description }}</n-text>

              <template #action>
                <n-button
                  type="primary"
                  block
                  secondary
                  @click="
                    executePlugin(plugin.commandCode, plugin.defaultPayload)
                  "
                >
                  Запустить
                </n-button>
              </template>
            </n-card>
          </n-grid-item>
        </n-grid>
      </n-layout-content>
    </n-layout>
  </n-config-provider>
</template>

<script setup>
import { ref } from "vue";
import { darkTheme, useMessage } from "naive-ui";

const message = useMessage();

const pluginList = ref([
  {
    id: 1,
    name: "Wall Checker",
    description: "Тестовый плагин для проверки связи с Revit API.",
    commandCode: "WallCheck",
    defaultPayload: { "test": "HelloWorld" },
    isStable: true,
  },
  {
    id: 2,
    name: "Экспорт PDF",
    description: "Пакетный экспорт всех листов проекта в формате PDF.",
    commandCode: "ExportToPdf",
    defaultPayload: { format: "A4", merge: true },
    isStable: true,
  },
  {
    id: 3,
    name: "Анализ коллизий",
    description: "Поиск пересечений между архитектурой и инженерными сетями.",
    commandCode: "IntersectionCheck",
    defaultPayload: { tolerance: 5.0 },
    isStable: false,
  },
  {
    id: 4,
    name: "Нумерация помещений",
    description: "Автоматическая сквозная нумерация по заданному сплайну.",
    commandCode: "NumberRooms",
    defaultPayload: { prefix: "A-" },
    isStable: true,
  },
]);

const executePlugin = (command, payload) => {
  const requestData = {
    plugin: command,
    payload: payload,
  };

  if (window.chrome && window.chrome.webview) {
    window.chrome.webview.postMessage(requestData);
  } else {
    console.warn("[DEV MODE] Сообщение для Revit:", requestData);
  }
};
</script>
