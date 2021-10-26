import { v4 as uuid } from "uuid";
export default {
  namespaced: true,
  state: {
    posts: [],
  },
  mutations: {
    ADD_POST(state, post) {
      state.posts.push(post);
    },
    DELETE_POST(state, id) {
      const index = state.posts.findIndex((i) => i.id === id);
      if (index === -1) {
        throw new Error("post no encontrado: ", id);
      }
      state.posts.splice(index, 1);
    },
  },
  actions: {
    addPost({ commit }, post) {
      return new Promise((resolve, reject) => {
        try {
          post.id = uuid();
          commit("ADD_POST", post);
          resolve(true);
        } catch (error) {
          reject(error);
        }
      });
    },
    deletePost({ commit }, id) {
      return new Promise((resolve, reject) => {
        try {
          commit("DELETE_POST", id);
          resolve(true);
        } catch (error) {
          reject(error);
        }
      });
    },
  },
};
