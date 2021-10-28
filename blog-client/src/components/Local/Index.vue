<template>
  <b-container class="post-container">
    <template v-if="getAllPosts.length > 0">
      <div v-for="(post, index) in getAllPosts" :key="index">
        <Post
          :id="post.id"
          :title="post.title"
          :content="post.content"
          :createdAt="post.createdAt"
          :updatedAt="post.updatedAt"
          :image="post.image"
          v-on:delete-post="deletePostInformation"
        />
      </div>
    </template>
    <template v-else>
      <h1>No hay posts registrados</h1>
    </template>
  </b-container>
</template>
<script>
import { mapActions, mapMutations } from "vuex";
import blogType from "@/utils/blogType";
import Post from "@/components/Post/Index";
export default {
  name: "Local",
  data() {
    return {
      posts: [],
    };
  },
  components: {
    Post,
  },
  computed: {
    getAllPosts() {
      return this.posts;
    },
  },
  watch: {
    posts: function (newVal) {
      this.posts = newVal;
    }
  },
  mounted() {
    this.$store.commit("blog/SET_BLOG_TYPE", blogType.LOCAL);
    this.posts = JSON.parse(localStorage.getItem("posts"));
  },
  methods: {
    ...mapMutations(["blog/SET_BLOG_TYPE"]),
    ...mapActions({
      deletePost: "local/deletePost",
    }),
    deletePostInformation(id) {
      this.$swal
        .fire({
          title: "¿Estás seguro de eliminar el post?",
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          cancelButtonText: "Cancelar",
          confirmButtonText: "Estoy seguro",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            try {
              const res = await this.deletePost(id);
              if (res) {
                this.$swal.fire(
                  "Eliminado",
                  "Tu post ha sido eliminado.",
                  "success"
                );
                const index = this.posts.findIndex((i) => i.id === id);
                this.posts.splice(index, 1);
              }
            } catch (error) {
              console.log(error);
              this.$swal.fire("Oops", "Tu post no fue eliminado.", "error");
            }
          }
        });
    },
  },
};
</script>
