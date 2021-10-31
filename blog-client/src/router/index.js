import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    redirect: "/local",
  },
  {
    path: "/local",
    name: "Local",
    component: () => import("../components/Local/Index.vue"),
  },
  {
    path: "/remote",
    name: "Remote",
    component: () => import("../components/Remote/Index.vue"),
  },
  {
    path: "/post/create",
    name: "NewPost",
    component: () => import("../components/Local/RegisterPost.vue"),
  },
  {
    path: "/post/remote/create",
    name: "NewRemotePost",
    component: () => import("../components/RemotePlus/RegisterPost.vue"),
  },
  {
    path: "/post/:id",
    name: "ViewPost",
    component: () => import("../views/ViewPost.vue"),
  },
  {
    path: "/post/remote/:id/edit",
    name: "ViewRemotePost",
    component: () => import("../components/RemotePlus/EditPost.vue"),
  },
  {
    path: "/post/:id/edit",
    name: "UpdatePost",
    component: () => import("../components/Local/EditPost.vue"),
  },
  {
    path: "/post/remote/:id",
    name: "UpdateRemotePost",
    component: () => import("../components/RemotePlus/EditPost.vue"),
  },
  {
    path: "/remote_plus",
    name: "RemotePlus",
    component: () => import("../components/RemotePlus/Index.vue"),
  },
  {
    path: "/:catchAll(.*)",
    redirect: "/local",
  },
  // {
  //   path: "/about",
  //   name: "About",
  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () =>
  //     import(/* webpackChunkName: "about" */ "../views/About.vue"),
  // },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
