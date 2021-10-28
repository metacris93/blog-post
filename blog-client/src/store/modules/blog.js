export default {
  namespaced: true,
  // state: {
  //   blogType: null,
  // },
  mutations: {
    SET_BLOG_TYPE(state, type) {
      state.blogType = type;
      localStorage.blogType = type;
    },
  },
  getters: {
    getBlogType() {
      return localStorage.blogType;
    },
  },
};
