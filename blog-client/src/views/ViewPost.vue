<template>
  <main v-if="this.post">
    <h1>{{ getTitle }}</h1>
    <figure>
      <img :src="getImage" alt="Image Post" />
    </figure>
    <div class="post-date d-flex justify-content-around">
      <div>
        <em>{{ getCreatedAt }}</em>
      </div>
      <div v-if="hasPostUpdated">
        <em>{{ showIfPostWasUpdated }}</em>
      </div>
    </div>
    <article>
      {{ getContent }}
    </article>
  </main>
  <main v-else>
    <h1>Post No Encontrado</h1>
  </main>
</template>
<script>
export default {
  data() {
    return {
      defaultImage: "https://picsum.photos/id/1019/200/200",
      post: null,
    };
  },
  computed: {
    getTitle() {
      return this.post.title ?? "";
    },
    getCreatedAt() {
      if (this.post.createdAt) return `Creado en: ${this.post.createdAt}`;
      return "";
    },
    getContent() {
      return this.post.content ?? "";
    },
    getImage() {
      if (this.post.image) return this.post.image;
      return this.defaultImage;
    },
    hasPostUpdated() {
      return this.post.updatedAt ?? false;
    },
    showIfPostWasUpdated() {
      return `Actualizado en: ${this.post.updatedAt}`;
    },
  },
  mounted() {
    try {
      if (localStorage.getItem("posts")) {
        const posts = JSON.parse(localStorage.getItem("posts"));
        const id = this.$route.params.id;
        const post = posts.find((item) => item.id === id);
        if (!post) {
          throw new Error("post no encontrado");
        }
        this.post = post;
      }
    } catch (error) {
      console.error(error);
    }
  },
};
</script>
<style lang="stylus">
h1
  padding-top 2.5rem
img
  margin-top 1rem
.post-date
  margin-top 1.5rem
article
  margin-top 2rem
  font-size 1.5em
  font-family Trirong, sans-serif
</style>
