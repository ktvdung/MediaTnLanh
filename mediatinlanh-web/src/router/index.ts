import { createRouter, createWebHistory } from 'vue-router';
import type { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'Home',
    component: () => import('../views/HomeView.vue'),
  },
  {
    path: '/thanh-ca',
    name: 'ThanhCa',
    component: () => import('../views/ThanhCaView.vue'),
  },
  {
    path: '/kinh-thanh',
    name: 'KinhThanh',
    component: () => import('../views/KinhThanhView.vue'),
  },
  {
    path: '/karaoke',
    name: 'Karaoke',
    component: () => import('../views/KaraokeView.vue'),
  },
  {
    path: '/quiz',
    name: 'Quiz',
    component: () => import('../views/QuizView.vue'),
  },
  {
    path: '/settings',
    name: 'Settings',
    component: () => import('../views/SettingsView.vue'),
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
