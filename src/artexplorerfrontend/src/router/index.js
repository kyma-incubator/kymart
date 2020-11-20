import { createRouter, createWebHistory } from 'vue-router';
import { vuexOidcCreateRouterMiddleware } from 'vuex-oidc';
import Home from '../views/Home.vue';
import OidcCallback from '../views/OidcCallback.vue';
import OidcPopupCallback from '../views/OidcPopupCallback.vue';
import OidcCallbackError from '../views/OidcCallbackError.vue';
import store from '@/store/index.js'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ '../views/About.vue'),
  },
  {
    path: '/protected',
    name: 'protected',
    component: () =>
      import(/* webpackChunkName: "protected" */ '../views/Protected.vue'),
  },
  {
    path: '/oidc-callback', // Needs to match redirectUri in you oidcSettings
    name: 'oidcCallback',
    component: OidcCallback,
  },
  {
    path: '/oidc-popup-callback', // Needs to match popupRedirectUri in you oidcSettings
    name: 'oidcPopupCallback',
    component: OidcPopupCallback,
  },
  {
    path: '/oidc-callback-error', // Needs to match redirect_uri in you oidcSettings
    name: 'oidcCallbackError',
    component: OidcCallbackError,
    meta: {
      isPublic: true,
    },
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach(vuexOidcCreateRouterMiddleware(store, 'oidcStore'));
export default router;
